using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PassView : BaseView
{
    public override void Open(params object[] args)
    {
        base.Open(args);
    }

    protected override void OnAwake()
    {
        base.OnAwake();

        Find<Button>("bg/ExitBtn").onClick.AddListener(onExitBtn);
    }

    private void onExitBtn()
    {
        GameApp.ViewManager.Close(ViewId);

        LevelLoader.Instance.LoadNextLevel("ChoiceMenu");
    }

}
