using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ����һЩ��Ϸͨ��UI�Ŀ�����(������� ��ʾ��� ��ʼ��Ϸ���������������ע��
/// </summary>
public class GameUIController : BaseController
{
    public GameUIController() : base()
    {
        //ע����ͼ
        GameApp.ViewManager.Register(ViewType.StartView, new ViewInfo()
        {
            PrefabName = "StartView",
            controller = this,
            parentTF = GameApp.ViewManager.canvasTF
            
        });
        //GameApp.ViewManager.Register(ViewType.MessageView, new ViewInfo()
        //{
        //    PrefabName = "MessageView",
        //    controller = this,
        //    Sorting_Order = 999,
        //    parentTF = GameApp.ViewManager.canvasTF
        //});
        InitModuleEvent();//��ʼ��ģ���¼�
        InitGlobalEvent();//��ʼ��ȫ���¼�

    }

    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.OpenStartView, openStartView);//ע��򿪿�ʼ���
        //RegisterFunc(Defines.OpenSetView, openSetView);//ע���������
        //RegisterFunc(Defines.OpenMessageView, openMessageView);//ע����ʾ���
    }

    //����ģ��ע���¼� ����

    private void openStartView(System.Object[] arg)
    {
        GameApp.ViewManager.Open(ViewType.StartView, arg);
        GameApp.ViewManager.Close(ViewType.StartView, arg);
    }

    //���������
    private void openSetView(System.Object[] arg)
    {
        GameApp.ViewManager.Open(ViewType.SetView, arg);
    }

    private void openMessageView(System.Object[] arg)
    {
        GameApp.ViewManager.Open(ViewType.MessageView, arg);
    }
}
