using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : BaseController
{
    AsyncOperation asyncOp;

    public LoadingController() : base()
    {
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

        asyncOp = SceneManager.LoadSceneAsync(loadingModel.SceneName);

        asyncOp.completed += onLoadedEndCallBack;
    }

    private void onLoadedEndCallBack(AsyncOperation asyncOp)
    {
        asyncOp.completed -= onLoadedEndCallBack;

        GetModel<LoadingModel>().callback?.Invoke(); //Ö´ÐÐ»Øµ÷

        
    }
}
