using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuButton : MonoBehaviour
{
    private GameObject menuPanel;
    // Start is called before the first frame update
    void Start()
    {
        menuPanel = GameObject.Find("PauseMenu");
    }

    //重新开始游戏
    public void RestartGame()
    {
        // 恢复游戏时间
        Time.timeScale = 1;

        // 重新加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //退出游戏
    public void QuitGame()
    {
        Debug.Log("退出中.....");
        Application.Quit();
    }
    //设置
    public void Setting()
    {

    }
    //继续游戏
    public void ResumeGame()
    {
        Time.timeScale = 1;          // 恢复游戏
        menuPanel.SetActive(false);  // 隐藏菜单
    }
    //选择关卡
    string sceneName = "ChoiceMenu";
    public void Loadlevel() {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}
