using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollController : MonoBehaviour
{
    public Camera mainCamera;           // 主摄像机
    public float scrollSpeed = 5f;      // 控制摄像机移动的速度
    private float minX = 0f;             // x轴最小值限制
    private float maxX = 31f;

    void Update()
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        // 检测鼠标是否在左侧区域
        if (Input.mousePosition.x <= 200f )
        {
            cameraPosition.x -= scrollSpeed * Time.deltaTime;
            cameraPosition.x = Mathf.Max(cameraPosition.x, minX); // 限制x不小于0
            mainCamera.transform.position = cameraPosition;
        }
        // 检测鼠标是否在右侧区域
        else if (Input.mousePosition.x >= 1750f)
        {
            // 向右移动摄像头
            cameraPosition.x += scrollSpeed * Time.deltaTime;
            cameraPosition.x = Mathf.Min(cameraPosition.x, maxX);
            mainCamera.transform.position = cameraPosition;
        }
        
        // 更新摄像头的位置
       
    }
}
