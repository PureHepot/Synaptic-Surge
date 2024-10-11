using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour
{
    private static readonly T instance = Activator.CreateInstance<T>();

    public static T Instance
    {
        get
        {
            return instance;
        }
    }
}
