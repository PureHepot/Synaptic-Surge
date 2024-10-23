using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PauseView : BaseView
{
    public override void Close(params object[] args)
    {
        base.Close(args);
        Time.timeScale = 1f;
    }

    public override void Open(params object[] args)
    {
        base.Open(args);
        Time.timeScale = 0.1f;
    }

    protected override void OnAwake()
    {
        base.OnAwake();

        Find<Button>("bg/ResumeButton").onClick.AddListener(onResumeBtn);
        Find<Button>("bg/RestartButton").onClick.AddListener(onRestartBtn);
        Find<Button>("bg/ExitButton").onClick.AddListener(onExitBtn);
    }

    private void onResumeBtn()
    {
        GameApp.ViewManager.Close(ViewId);
        GameManager.isPause = !GameManager.isPause;
    }

    private void onRestartBtn()
    {
        LevelLoader.Instance.LoadNextLevel(SceneManager.GetActiveScene().name);
        GameApp.ViewManager.Close(ViewId);
    }

    private void onExitBtn()
    {
        LevelLoader.Instance.LoadNextLevel("ChoiceMenu");
        GameApp.ViewManager.Close(ViewId);
    }

}
