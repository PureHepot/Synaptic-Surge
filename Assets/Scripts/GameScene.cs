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
        //���������ʽ
        //Cursor.SetCursor(mouseTxt, Vector2.zero, CursorMode.Auto);

        //GameApp.SoundManager.PlayBGM("login");

        RegisterModule();//ע����Ϸ�еĿ�����

        InitModule();
    }

    private void RegisterModule()
    {
        GameApp.ControllerManager.Register(ControllerType.GameUI, new GameUIController());
        GameApp.ControllerManager.Register(ControllerType.Game, new GameController());
    }

    /// <summary>
    /// ִ�����п�������ʼ��
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
