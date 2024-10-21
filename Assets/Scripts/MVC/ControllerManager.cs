using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


/// <summary>
/// ������������
/// </summary>
public class ControllerManager
{
    private Dictionary<int, BaseController> _modules;//�洢���������ֵ�

    public ControllerManager()
    {
        _modules = new Dictionary<int, BaseController>();  
    }


    public void Register(ControllerType type, BaseController controller)
    {
        Register((int)type,controller);
    }

    /// <summary>
    /// ע�������
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
    /// �Ƴ�������
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
    /// ���
    /// </summary>
    public void Clear()
    {
        _modules.Clear();
    }

    /// <summary>
    /// ������п�����
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
    /// ��ģ�崥����Ϣ
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
    /// ��ȡĳ��������model����
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
