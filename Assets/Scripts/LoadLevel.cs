using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;  //引用Editor来使用SceneAsset
using UnityEngine.SceneManagement;
public class LoadLevel : MonoBehaviour
{
    // 通过编辑器选择场景
    [SerializeField] private SceneAsset sceneToLoad;

    
    // 加载场景
    public void Loadlevel()
    {
        if (sceneToLoad != null)
        {
            // 使用SceneAsset的名称来加载场景
            string sceneName = sceneToLoad.name;
            Debug.Log("Loading Scene: " + sceneName);
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("No scene selected!");
        }
    }
}
