using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PassView : BaseView
{
    public override void Open(params object[] args)
    {
        base.Open(args);
    }

    protected override void OnAwake()
    {
        base.OnAwake();

        Find<Button>("bg/ExitBtn").onClick.AddListener(onExitBtn);
    }

    private void onExitBtn()
    {
        GameApp.ViewManager.Close(ViewId);

        LevelLoader.Instance.LoadNextLevel("ChoiceMenu");

        int a = int.Parse(Regex.Match(SceneManager.GetActiveScene().name, @"\d+").Value);

        GameScene.gameData.level = a > GameScene.gameData.level ? a + 1 : GameScene.gameData.level;

        SaveSystem.SaveGame(GameScene.gameData);
    }

}
