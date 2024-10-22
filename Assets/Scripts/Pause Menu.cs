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
        // ¼ì²âÊÇ·ñ°´ÏÂESC¼ü
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CheckMenu();  // ¼ì²â²¢ÇÐ»»²Ëµ¥×´Ì¬
        }
    }

    void CheckMenu() {
        isMenuActive = !isMenuActive;
        menuPanel.SetActive(isMenuActive);  // ÏÔÊ¾»òÒþ²Ø²Ëµ¥

        // ÔÝÍ£»ò»Ö¸´ÓÎÏ·
        if (isMenuActive)
        {
            Time.timeScale = 0;  // ÔÝÍ£ÓÎÏ·
        }
        else
        {
            Time.timeScale = 1;  // »Ö¸´ÓÎÏ·
        }
    }

    /*
    
    */

}
