using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

//视图信息类
public class ViewInfo
{
    public string PrefabName; //视图预制件名称
    public Transform parentTF;//所在父级
    public BaseController controller;//视图所属控制器
    public int Sorting_Order;//显示层级
}

/// <summary>
/// 视图管理器
/// </summary>
public class ViewManager : MonoBehaviour
{
    public Transform canvasTF;//画布组件
    Dictionary<int, IBaseView> _opens;//开启中的视图
    Dictionary<int, IBaseView> _viewsCache;//视图缓存
    Dictionary<int, ViewInfo> _views;//注册的试图信息

    public ViewManager()
    {
        canvasTF = GameObject.Find("Canvas").transform;
        _opens = new Dictionary<int, IBaseView>();
        _views = new Dictionary<int, ViewInfo>();
        _viewsCache = new Dictionary<int, IBaseView>();
    }

    /// <summary>
    /// 注册试图信息
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
    /// 移除试图信息
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
    /// 移除面板
    /// </summary>
    /// <param name="key"></param>
    public void RemoveView(int key)
    {
        _views.Remove(key);
        _viewsCache.Remove(key);
        _opens.Remove(key);
    }

    /// <summary>
    /// 移除控制器中的面板视图
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
    /// 是否开启中
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public bool IsOpen(int key)
    {
        return _opens.ContainsKey(key);
    }


    /// <summary>
    /// 获得某个视图
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
    /// 销毁视图
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
    /// 关闭面板视图
    /// </summary>
    /// <param name="key"></param>
    /// <param name="args"></param>
    public void Close(int key, params object[] args)
    {
        //没打开
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
    /// 打开面板
    /// </summary>
    /// <param name="type"></param>
    /// <param name="args"></param>
    public void Open(ViewType type, params object[] args)
    {
        Open((int)type, args);
    }


    /// <summary>
    /// 打开某个视图面板
    /// </summary>
    /// <param name="key"></param>
    /// <param name="args"></param>
    public void Open(int key, params object[] args)
    {
        IBaseView view = GetView(key);
        ViewInfo viewInfo = _views[key];
        if(view == null)
        {
            //不存在的视图 进行资源加载
            string type = ((ViewType)key).ToString();//类型的字符串跟脚本名称对应
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
            canvas.overrideSorting = true;//可以设置层级
            canvas.sortingOrder = viewInfo.Sorting_Order;//设置层级
            view = uiObj.AddComponent(Type.GetType(type)) as IBaseView;//添加对应View脚本
            view.ViewId = key;//视图id
            view.Controller = viewInfo.controller;//设置控制器
            //添加到视图缓存
            _viewsCache.Add(key, view);
            viewInfo.controller.OnLoadView(view);
        }
        //已经打开了视图
        if (this._opens.ContainsKey(key) == true)
        {
            return;
        }
        this._opens.Add(key, view);

        //已经初始化过
        if(view.IsInit())
        {
            view.SetVisible(true);//显示
            view.Open(args);//打开
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
