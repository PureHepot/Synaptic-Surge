using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 处理一些游戏通用UI的控制器(设置面板 提示面板 开始游戏面板等在这个控制器注册
/// </summary>
public class GameUIController : BaseController
{
    public GameUIController() : base()
    {
        //注册视图
        GameApp.ViewManager.Register(ViewType.StartView, new ViewInfo()
        {
            PrefabName = "StartView",
            controller = this,
            parentTF = GameApp.ViewManager.canvasTF
            
        });
        GameApp.ViewManager.Register(ViewType.PauseView, new ViewInfo()
        {
            PrefabName = "PauseView",
            controller = this,
            Sorting_Order = 999,
            parentTF = GameApp.ViewManager.canvasTF
        });
        GameApp.ViewManager.Register(ViewType.PassView, new ViewInfo()
        {
            PrefabName = "PassView",
            controller = this,
            Sorting_Order = 10,
            parentTF = GameApp.ViewManager.canvasTF
        });

        GameApp.ViewManager.Register(ViewType.EnddingView, new ViewInfo()
        {
            PrefabName = "EnddingView",
            controller = this,
            Sorting_Order = 100,
            parentTF = GameApp.ViewManager.canvasTF
        });
        InitModuleEvent();//初始化模板事件
        InitGlobalEvent();//初始化全局事件

    }

    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.OpenStartView, openStartView);//注册打开开始面板
        RegisterFunc(Defines.OpenPassView, openPassView);
        //RegisterFunc(Defines.OpenSetView, openSetView);//注册设置面板
        //RegisterFunc(Defines.OpenMessageView, openMessageView);//注册提示面板
    }

    //测试模板注册事件 例子

    private void openStartView(System.Object[] arg)
    {
        GameApp.ViewManager.Open(ViewType.StartView, arg);
    }

    //打开设置面板
    private void openSetView(System.Object[] arg)
    {
        GameApp.ViewManager.Open(ViewType.SetView, arg);
    }

    private void openMessageView(System.Object[] arg)
    {
        GameApp.ViewManager.Open(ViewType.MessageView, arg);
    }

    private void openPassView(System.Object[] arg)
    {
        GameApp.ViewManager.Open(ViewType.PassView, arg);
    }

    private void openEnddingView(System.Object[] arg)
    {
        GameApp.ViewManager.Open(ViewType.EnddingView, arg);
    }
}
