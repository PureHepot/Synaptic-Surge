using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    private Animator m_Animator;
    public static LevelLoader Instance;
    public float waitTime;

    private void Awake()
    {
        m_Animator = GetComponentInChildren<Animator>();
        Instance = this;
    }

    void Update()
    {
        
    }

    public void LoadNextLevel(string sceneName)
    {
        StartCoroutine(LoadLevel(sceneName));
    }

    IEnumerator LoadLevel(string sceneName)
    {
        m_Animator.SetTrigger("Start");

        yield return new WaitForSeconds(waitTime);

        SceneManager.LoadScene(sceneName);
    }
}
