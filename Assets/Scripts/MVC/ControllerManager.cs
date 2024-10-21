using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// 控制器管理器
/// </summary>
public class ControllerManager
{
    private Dictionary<int, BaseController> _modules;//存储控制器的字典

    public ControllerManager()
    {
        _modules = new Dictionary<int, BaseController>();  
    }


    public void Register(ControllerType type, BaseController controller)
    {
        Register((int)type,controller);
    }

    /// <summary>
    /// 注册控制器
    /// </summary>
    /// <param name="controllerKey"></param>
    /// <param name="controller"></param>
    public void Register(int controllerKey, BaseController controller)
    {
        if(_modules.ContainsKey(controllerKey) ==false)
        {
            _modules.Add(controllerKey, controller);
        }
    }

    public void InitAllModules()
    {
        foreach (var item in _modules)
        {
            item.Value.Init();
        }
    }

    /// <summary>
    /// 移除控制器
    /// </summary>
    /// <param name="controllerKey"></param>
    public void UnRegister(int controllerKey)
    {
        if(_modules.ContainsKey(controllerKey))
        {
            _modules.Remove(controllerKey);
        }
    }
    
    /// <summary>
    /// 清除
    /// </summary>
    public void Clear()
    {
        _modules.Clear();
    }

    /// <summary>
    /// 清除所有控制器
    /// </summary>
    public void ClearAllModules()
    {
        List<int> keys = _modules.Keys.ToList();
        for(int i =0;i< keys.Count;i++)
        {
            _modules[keys[i]].Destroy();
            _modules.Remove(keys[i]);
        }
    }

    /// <summary>
    /// 跨模板触发消息
    /// </summary>
    /// <param name="controllerKey"></param>
    /// <param name="eventName"></param>
    /// <param name="args"></param>
    public void ApplyFunc(int controllerKey, string eventName, System.Object[] args)
    {
        if(_modules.ContainsKey(controllerKey))
        {
            _modules[controllerKey].ApplyFunc(eventName, args);
        }
           
    }
    /// <summary>
    /// 获取某控制器的model对象
    /// </summary>
    /// <param name="controllerKey"></param>
    /// <returns></returns>
    public BaseModel GetControllerModel(int controllerKey)
    {
        if(_modules.ContainsKey(controllerKey))
        {
            return _modules[controllerKey].GetModel();
        }
        else
        {
            return null;
        }
    }
}
