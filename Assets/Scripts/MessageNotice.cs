using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MessageNotice : MonoBehaviour
{
    public Vector3 genetatePosition;
    
    public string message;

    private Text txt;

    public float amplitude = 3.0f; // 振幅，决定了移动的最大高度
    public float frequency = 1.0f; // 频率，决定了周期的快慢

    private Vector3 startPosition;

    private void Awake()
    {
        txt = GetComponentInChildren<Text>();
        
    }

    void Start()
    {
        txt.text = message;
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float y = Mathf.Sin(Time.time * frequency) * amplitude;
        txt.transform.position = startPosition + new Vector3(0, y, 0);

        if(Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
    }

    private void FirstLight()
    {
        GameApp.ViewManager.Open(ViewType.StartView);
    }


}
