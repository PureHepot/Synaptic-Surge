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
        Find<Button>("bg/ContinueButton").onClick.AddListener(onContinueBtn);
        Find<Button>("bg/ExitButton").onClick.AddListener(onQuitGameBtn);
    }

    //开始游戏
    private void onStartGameBtn()
    {
        LevelLoader.Instance.LoadNextLevel("ChoiceMenu");
    }
    //打开设置
    private void onContinueBtn()
    {
        
    }
    //退出游戏
    private void onQuitGameBtn()
    {
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
