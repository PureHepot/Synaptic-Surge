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

    //���¿�ʼ��Ϸ
    public void RestartGame()
    {
        // �ָ���Ϸʱ��
        Time.timeScale = 1;

        // ���¼��ص�ǰ����
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    //�˳���Ϸ
    public void QuitGame()
    {
        Debug.Log("�˳���.....");
        Application.Quit();
    }
    //����
    public void Setting()
    {

    }
    //������Ϸ
    public void ResumeGame()
    {
        Time.timeScale = 1;          // �ָ���Ϸ
        menuPanel.SetActive(false);  // ���ز˵�
    }
    //ѡ��ؿ�
    string sceneName = "ChoiceMenu";
    public void Loadlevel() {
        Time.timeScale = 1;
        SceneManager.LoadScene(sceneName);
    }
}
