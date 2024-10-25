using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private List<LaserLight> lights;

    private bool isLevel;

    public static bool isPause;
    public static bool isPass;

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
        isPass = false;
        foreach (GameObject obj in objs)
        {
            lights.Add(obj.GetComponent<LaserLight>());
        }
        string letters = Regex.Match(SceneManager.GetActiveScene().name, @"[a-zA-Z]+").Value;
        if(letters == "Level") isLevel = true;
        else isLevel = false;
    }

    private void Update()
    {
        if (isAllLightPowerOn())
        {
            //通关
            StartCoroutine(PasstheLevel());
        }
        if(isLevel && Input.GetKeyDown(KeyCode.Escape))
        {
            if(!isPause) GameApp.ViewManager.Open(ViewType.PauseView);
            else GameApp.ViewManager.Close(ViewType.PauseView);
            isPause = !isPause;
        }
    }

    private bool isAllLightPowerOn()
    {
        if(lights.Count <= 0) return false; 
        if (isPass) return false;

        foreach (LaserLight obj in lights)
        {
            if (obj.isPowerOn == false)
                return false;
        }
        isPass = true;
        return true;
    }


    IEnumerator PasstheLevel()
    {
        yield return new WaitForSeconds(2);
        GameApp.ViewManager.Open(ViewType.PassView);
    }

}
