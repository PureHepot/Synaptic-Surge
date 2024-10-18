using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// ����������
/// </summary>
public class BaseController
{
    private Dictionary<string, System.Action<object[]>> message;//�¼��ֵ�

    protected BaseModel model;//ģ������

    public BaseController()
    {
        message = new Dictionary<string, System.Action<object[]>>();
    }

    /// <summary>
    /// ע�����õĳ�ʼ��������Ҫ���п�������ʼ����ִ�У�
    /// </summary>
    public virtual void Init()
    {

    }


    /// <summary>
    /// ������ͼ
    /// </summary>
    /// <param name="view"></param>
    public virtual void OnLoadView(IBaseView view) { }

    /// <summary>
    /// ����ͼ
    /// </summary>
    /// <param name="view"></param>
    public virtual void OpenView(IBaseView view) 
    {

    }

    /// <summary>
    /// �ر���ͼ
    /// </summary>
    /// <param name="view"></param>
    public virtual void CloseView(IBaseView view)
    {

    }

    /// <summary>
    /// ע��ģ���¼�
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="callback"></param>
    public void RegisterFunc(string eventName, System.Action<object[]> callback)
    {
        if(message.ContainsKey(eventName))
        {
            message[eventName] += callback;
        }
        else
        {
            message.Add(eventName, callback);
        }
    }
    
    /// <summary>
    /// �Ƴ�ģ���¼�
    /// </summary>
    /// <param name="eventName"></param>
    public void UnRegisterFunc(string eventName)
    {
        if(message.ContainsKey(eventName))
        {
            message.Remove(eventName);
        }
    }

    /// <summary>
    /// ������ģ���¼�
    /// </summary>
    /// <param name="eventName"></param>
    /// <param name="args"></param>
    public void ApplyFunc(string eventName, params object[] args)
    {
        if(message.ContainsKey(eventName))
        {
            message[eventName].Invoke(args);
        }
        else
        {
            Debug.LogError("error" + eventName);
        }
    }

    /// <summary>
    /// ��������ģ����¼�
    /// </summary>
    /// <param name="controllerKey"></param>
    /// <param name="eventName"></param>
    /// <param name="args"></param>
    public void ApplyControllerFunc(int controllerKey, string eventName, params object[] args)
    {
        GameApp.ControllerManager.ApplyFunc(controllerKey, eventName, args);
    }
    public void ApplyControllerFunc(ControllerType type, string eventName, params object[] args)
    {
        ApplyControllerFunc((int)type, eventName, args);
    }

    /// <summary>
    /// ����ģ������
    /// </summary>
    /// <param name="model"></param>
    public void SetModel(BaseModel model)
    {
        this.model = model;
        this.model.controller = this;
    }

    /// <summary>
    /// ���ģ������
    /// </summary>
    /// <returns></returns>
    public BaseModel GetModel()
    {
        return this.model; 
    }

    public T GetModel<T>() where T : BaseModel
    {
        return model as T;
    }


    public BaseModel GetControllerModel(int controllerKey)
    {
        return GameApp.ControllerManager.GetControllerModel(controllerKey);
    }

    /// <summary>
    /// ɾ��������
    /// </summary>
    public virtual void Destroy()
    {
        RemoveModuleEvent();
        RemoveGlobalEvent(); 
    }

    /// <summary>
    /// ��ʼ��ģ���¼�
    /// </summary>
    public virtual void InitModuleEvent()
    {

    }

    /// <summary>
    /// �Ƴ�ģ���¼�
    /// </summary>
    public virtual void RemoveModuleEvent()
    {

    }

    /// <summary>
    /// ��ʼ��ȫ���¼�
    /// </summary>
    public virtual void InitGlobalEvent()
    {

    }

    /// <summary>
    /// �Ƴ�ȫ���¼�
    /// </summary>
    public virtual void RemoveGlobalEvent()
    {

    }
}
