using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

//��ͼ��Ϣ��
public class ViewInfo
{
    public string PrefabName; //��ͼԤ�Ƽ�����
    public Transform parentTF;//���ڸ���
    public BaseController controller;//��ͼ����������
    public int Sorting_Order;//��ʾ�㼶
}

/// <summary>
/// ��ͼ������
/// </summary>
public class ViewManager : MonoBehaviour
{
    public Transform canvasTF;//�������
    Dictionary<int, IBaseView> _opens;//�����е���ͼ
    Dictionary<int, IBaseView> _viewsCache;//��ͼ����
    Dictionary<int, ViewInfo> _views;//ע�����ͼ��Ϣ

    public ViewManager()
    {
        canvasTF = GameObject.Find("Canvas").transform;
        _opens = new Dictionary<int, IBaseView>();
        _views = new Dictionary<int, ViewInfo>();
        _viewsCache = new Dictionary<int, IBaseView>();
    }

    /// <summary>
    /// ע����ͼ��Ϣ
    /// </summary>
    /// <param name="key"></param>
    /// <param name="viewInfo"></param>
    public void Register(int key,ViewInfo viewInfo)
    {
        if(_views.ContainsKey(key)==false)
        {
            _views.Add(key, viewInfo);
        }
    }
    public void Register(ViewType viewType, ViewInfo viewInfo)
    {
        Register((int)viewType, viewInfo);  
    }

    /// <summary>
    /// �Ƴ���ͼ��Ϣ
    /// </summary>
    /// <param name="key"></param>
    public void UnRegister(int key)
    {
        if( _views.ContainsKey(key))
        {
            _views.Remove(key);
        }
    }

    /// <summary>
    /// �Ƴ����
    /// </summary>
    /// <param name="key"></param>
    public void RemoveView(int key)
    {
        _views.Remove(key);
        _viewsCache.Remove(key);
        _opens.Remove(key);
    }

    /// <summary>
    /// �Ƴ��������е������ͼ
    /// </summary>
    /// <param name="controller"></param>
    public void RemoveViewByController(BaseController controller)
    {
        foreach(var item in _views)
        {
            if(item.Value.controller == controller)
            {
                RemoveView(item.Key);
            }
        }
    }

    /// <summary>
    /// �Ƿ�����
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool IsOpen(int key)
    {
        return _opens.ContainsKey(key);
    }


    /// <summary>
    /// ���ĳ����ͼ
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public IBaseView GetView(int key)
    {
        if (_opens.ContainsKey(key))
        {
            return _opens[key];
        }
        if(_viewsCache.ContainsKey(key))
            return _viewsCache[key];

        return null;
    }

    public T GetView<T>(int key) where T:class, IBaseView
    {
        IBaseView view = GetView(key);
        if (view != null)
        {
            return view as T;
        }
        return null;
    }

    /// <summary>
    /// ������ͼ
    /// </summary>
    //public void OnDestroy(int key)
    //{
    //    IBaseView oldView = GetView(key);
    //    if (oldView != null)
    //    {
    //        UnRegister(key);
    //        oldView.DestroyView();
    //        _viewsCache.Remove(key);
    //    }
    //}

    /// <summary>
    /// �ر������ͼ
    /// </summary>
    /// <param name="key"></param>
    /// <param name="args"></param>
    public void Close(int key, params object[] args)
    {
        //û��
        if (IsOpen(key) == false)
        {
            return;
        }
        IBaseView view = GetView(key);
        if(view != null)
        {
            _opens.Remove(key);
            view.Close(args);
            _viewsCache[key].Controller.CloseView(view);
        }

    }

    public void Close(ViewType key, params object[] args)
    {
        Close((int)key, args);
    }

    /// <summary>
    /// �����
    /// </summary>
    /// <param name="type"></param>
    /// <param name="args"></param>
    public void Open(ViewType type, params object[] args)
    {
        Open((int)type, args);
    }


    /// <summary>
    /// ��ĳ����ͼ���
    /// </summary>
    /// <param name="key"></param>
    /// <param name="args"></param>
    public void Open(int key, params object[] args)
    {
        IBaseView view = GetView(key);
        ViewInfo viewInfo = _views[key];
        if(view == null)
        {
            //�����ڵ���ͼ ������Դ����
            string type = ((ViewType)key).ToString();//���͵��ַ������ű����ƶ�Ӧ
            GameObject uiObj = UnityEngine.Object.Instantiate(Resources.Load($"Views/{viewInfo.PrefabName}"),viewInfo.parentTF) as GameObject;
            Canvas canvas = uiObj.GetComponent<Canvas>();
            if(canvas == null )
            {
                canvas = uiObj.AddComponent<Canvas>();
            }
            if (uiObj.GetComponent<GraphicRaycaster>() == null)
            {
                uiObj.AddComponent<GraphicRaycaster>();
            }
            canvas.overrideSorting = true;//�������ò㼶
            canvas.sortingOrder = viewInfo.Sorting_Order;//���ò㼶
            view = uiObj.AddComponent(Type.GetType(type)) as IBaseView;//��Ӷ�ӦView�ű�
            view.ViewId = key;//��ͼid
            view.Controller = viewInfo.controller;//���ÿ�����
            //��ӵ���ͼ����
            _viewsCache.Add(key, view);
            viewInfo.controller.OnLoadView(view);
        }
        //�Ѿ�������ͼ
        if (this._opens.ContainsKey(key) == true)
        {
            return;
        }
        this._opens.Add(key, view);

        //�Ѿ���ʼ����
        if(view.IsInit())
        {
            view.SetVisible(true);//��ʾ
            view.Open(args);//��
            viewInfo.controller.OpenView(view);
        }
        else
        {
            view.InitUI();
            view.InitData();
            view.Open(args);
            viewInfo.controller.OpenView(view);
        }

    }
}
