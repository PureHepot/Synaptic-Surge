using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private bool isMenuActive=false;
    public GameObject menuPanel;
    // Start is called before the first frame update
    void Start()
    {
        menuPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // ����Ƿ���ESC��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CheckMenu();  // ��Ⲣ�л��˵�״̬
        }
    }

    void CheckMenu() {
        isMenuActive = !isMenuActive;
        menuPanel.SetActive(isMenuActive);  // ��ʾ�����ز˵�

        // ��ͣ��ָ���Ϸ
        if (isMenuActive)
        {
            Time.timeScale = 0;  // ��ͣ��Ϸ
        }
        else
        {
            Time.timeScale = 1;  // �ָ���Ϸ
        }
    }

    /*
    
    */

}
