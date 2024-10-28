using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FunctionScri : MonoBehaviour
{
    public GameObject message1;
    private bool message1_ = true;

    public GameObject message2;
    private bool message2_ = true;

    public GameObject message3;
    private bool message3_ = true;

    // Update is called once per frame
    void Update()
    {
        if(message1_ && GameScene.gameData.level == 1)
        {
            message1.SetActive(true);
            message1_ = false;
        }
        if (message2_ && GameScene.gameData.level == 3)
        {
            message2.SetActive(true);
            message2_ = false;
        }
        if(message3_ && GameScene.gameData.level == 1) 
        {
            message3.SetActive(true);
            message3_ = false;
        }
    }
}
