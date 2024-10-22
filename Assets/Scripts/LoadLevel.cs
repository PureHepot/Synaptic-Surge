using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;  //����Editor��ʹ��SceneAsset
using UnityEngine.SceneManagement;
public class LoadLevel : MonoBehaviour
{
    // ͨ���༭��ѡ�񳡾�
    [SerializeField] private SceneAsset sceneToLoad;

    
    // ���س���
    public void Loadlevel()
    {
        if (sceneToLoad != null)
        {
            // ʹ��SceneAsset�����������س���
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
