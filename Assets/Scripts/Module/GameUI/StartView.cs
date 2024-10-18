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
        Find<Button>("setBtn").onClick.AddListener(onSetBtn);
        Find<Button>("bg/ExitButton").onClick.AddListener(onQuitGameBtn);
    }

    //��ʼ��Ϸ
    private void onStartGameBtn()
    {

    }
    //������
    private void onSetBtn()
    {
        //ApplyFunc(Defines.OpenSetView);
    }
    //�˳���Ϸ
    private void onQuitGameBtn()
    {
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
