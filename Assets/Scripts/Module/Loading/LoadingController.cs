using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingController : BaseController
{
    public LoadingController() : base()
    {
        GameApp.ViewManager.Register(ViewType.LoadingView, new ViewInfo(){
            PrefabName = "LoadingView",
            controller = this,
            parentTF = GameApp.ViewManager.canvasTF
        });

        InitModuleEvent();
    }

    public override void InitModuleEvent()
    {
        RegisterFunc(Defines.LoadingScene, LoadingSceneCallback);
    }

    private void LoadingSceneCallback(System.Object[] args)
    {
        LoadingModel loadingModel = args[0] as LoadingModel;

        SetModel(loadingModel);
    }
}
