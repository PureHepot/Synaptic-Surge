using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开始游戏界面
/// </summary>
public class StartView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();

        Find<Button>("bg/StartButton").onClick.AddListener(onStartGameBtn);
        Find<Button>("bg/ExitButton").onClick.AddListener(onQuitGameBtn);
    }

    //开始游戏
    private void onStartGameBtn()
    {
        //播放
        GameApp.SoundManager.PlayBGM(Defines.UIButton,false);
        GameApp.SoundManager.StopBgm(Defines.StartBgm);
        LevelLoader.Instance.LoadNextLevel("ChoiceMenu", ViewId);
        //LoadingModel loadingModel = new LoadingModel();
        //loadingModel.SceneName = "ChoiceMenu";
        //Controller.ApplyControllerFunc(ControllerType.Loading,Defines.LoadingScene,loadingModel);
    }
    //打开设置
    private void onContinueBtn()
    {
        GameApp.SoundManager.PlayBGM(Defines.UIButton, false);
    }
    //退出游戏
    private void onQuitGameBtn()
    {
        GameApp.SoundManager.PlayBGM(Defines.UIButton, false);

        Application.Quit();
        //Controller.ApplyControllerFunc(ControllerType.GameUI, Defines.OpenMessageView, new MessageInfo()
        //{
        //    okCallback = () =>
        //    {
        //        Application.Quit();
        //    },
        //    MsgTxt = "确定要退出游戏吗？"
        //});
    }
}
