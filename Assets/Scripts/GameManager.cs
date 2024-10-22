using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private List<LaserLight> lights;


    private void OnDestroy()
    {
        // 确保移除事件监听器，避免内存泄漏
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Light");
        lights = new List<LaserLight>();
        foreach (GameObject obj in objs)
        {
            lights.Add(obj.GetComponent<LaserLight>());
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Light");
        lights.Clear();
        foreach (GameObject obj in objs)
        {
            lights.Add(obj.GetComponent<LaserLight>());
        }
    }

    private void Update()
    {
        if (isAllLightPowerOn())
        {
            //通关
        }
    }

    private bool isAllLightPowerOn()
    {
        if(lights.Count <= 0) { return false; }

        foreach (LaserLight obj in lights)
        {
            if (obj.isPowerOn == false)
                return false;
        }

        return true;
    }

}
