using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

/// <summary>
/// 控制器基类
/// </summary>
public class BaseController
{
    private Dictionary<string, System.Action<object[]>> message;//事件字典

    protected BaseModel model;//模板数据

    public BaseController()
    {
        message = new Dictionary<string, System.Action<object[]>>();
    }

    /// <summary>
    /// 注册后调用的初始化函数（要所有控制器初始化后执行）
    /// </summary>
    public virtual void Init()
    {

    }


    /// <summary>
    /// 加载视图
    /// </summary>
    /// <param name="view"></param>
    public virtual void OnLoadView(IBaseView view) { }

    /// <summary>
    /// 打开视图
    /// </summary>
    /// <param name="view"></param>
    public virtual void OpenView(IBaseView view) 
    {

    }

    /// <summary>
    /// 关闭视图
    /// </summary>
    /// <param name="view"></param>
    public virtual void CloseView(IBaseView view)
    {

    }

    /// <summary>
    /// 注册模块事件
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
    /// 移除模块事件
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
    /// 触发本模块事件
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
    /// 触发其他模块的事件
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
    /// 设置模块数据
    /// </summary>
    /// <param name="model"></param>
    public void SetModel(BaseModel model)
    {
        this.model = model;
        this.model.controller = this;
    }

    /// <summary>
    /// 获得模块数据
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
    /// 删除控制器
    /// </summary>
    public virtual void Destroy()
    {
        RemoveModuleEvent();
        RemoveGlobalEvent(); 
    }

    /// <summary>
    /// 初始化模块事件
    /// </summary>
    public virtual void InitModuleEvent()
    {

    }

    /// <summary>
    /// 移除模块事件
    /// </summary>
    public virtual void RemoveModuleEvent()
    {

    }

    /// <summary>
    /// 初始化全局事件
    /// </summary>
    public virtual void InitGlobalEvent()
    {

    }

    /// <summary>
    /// 移除全局事件
    /// </summary>
    public virtual void RemoveGlobalEvent()
    {

    }
}
