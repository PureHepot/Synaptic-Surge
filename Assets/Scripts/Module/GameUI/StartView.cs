using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ��ʼ��Ϸ����
/// </summary>
public class StartView : BaseView
{
    protected override void OnAwake()
    {
        base.OnAwake();

        Find<Button>("bg/StartButton").onClick.AddListener(onStartGameBtn);
        Find<Button>("bg/ExitButton").onClick.AddListener(onQuitGameBtn);
    }

    //��ʼ��Ϸ
    private void onStartGameBtn()
    {
        //����
        GameApp.SoundManager.PlayBGM(Defines.UIButton,false);
        GameApp.SoundManager.StopBgm(Defines.StartBgm);
        LevelLoader.Instance.LoadNextLevel("ChoiceMenu", ViewId);
        //LoadingModel loadingModel = new LoadingModel();
        //loadingModel.SceneName = "ChoiceMenu";
        //Controller.ApplyControllerFunc(ControllerType.Loading,Defines.LoadingScene,loadingModel);
    }
    //������
    private void onContinueBtn()
    {
        GameApp.SoundManager.PlayBGM(Defines.UIButton, false);
    }
    //�˳���Ϸ
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
        //    MsgTxt = "ȷ��Ҫ�˳���Ϸ��"
        //});
    }
}
