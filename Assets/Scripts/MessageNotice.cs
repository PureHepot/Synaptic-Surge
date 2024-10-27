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

    public GameObject arrow;

    public float amplitude = 3.0f; // 振幅，决定了移动的最大高度
    public float frequency = 1.0f; // 频率，决定了周期的快慢

    private Vector3 startPosition;

    private void Awake()
    {
        txt = GetComponentInChildren<Text>();
        transform.parent.GetComponent<Canvas>().worldCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        
    }

    void Start()
    {
        txt.text = message;
        startPosition = arrow.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float y = Mathf.Sin(Time.time * frequency) * amplitude;
        arrow.transform.position = startPosition + new Vector3(0, y, 0);

    }

    private void FirstLight()
    {
        GameApp.ViewManager.Open(ViewType.StartView);
    }


}
