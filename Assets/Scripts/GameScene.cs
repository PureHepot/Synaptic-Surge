using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    public Texture2D mouseTxt;
    float dt;
    private static  bool isLoaded = false;

    public static GameData gameData = new GameData();
    private void Awake()
    {
        if (isLoaded)
        {
            Destroy(gameObject);
        }
        else
        {
            isLoaded = true;
            gameData = SaveSystem.LoadGame();
            gameData.level = 5;
            DontDestroyOnLoad(gameObject);
            GameApp.Instance.Init();
        }

        
    }

    private void Start()
    {
        //设置鼠标样式
        //Cursor.SetCursor(mouseTxt, Vector2.zero, CursorMode.Auto);

        //GameApp.SoundManager.PlayBGM("login");

        RegisterModule();//注册游戏中的控制器

        InitModule();
    }

    private void RegisterModule()
    {
        GameApp.ControllerManager.Register(ControllerType.GameUI, new GameUIController());
        GameApp.ControllerManager.Register(ControllerType.Game, new GameController());
    }

    /// <summary>
    /// 执行所有控制器初始化
    /// </summary>
    private void InitModule()
    {
        GameApp.ControllerManager.InitAllModules();
    }

    private void Update()
    {
        dt = Time.deltaTime;
        GameApp.Instance.Update(dt);
    }
}
