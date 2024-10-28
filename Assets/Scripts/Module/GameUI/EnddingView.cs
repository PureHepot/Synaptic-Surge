using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnddingView : BaseView
{
    public override void Open(params object[] args)
    {
        base.Open(args);
        GameApp.SoundManager.PlayBGM("StartBgm", false);
    }

    protected override void OnAwake()
    {
        Find<Button>("ExitBtn").onClick.AddListener(onExitBtn);
    }

    private void onExitBtn()
    {
        Application.Quit();
    }
}
