using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollController : MonoBehaviour
{
    public RectTransform leftZone;      // 左侧检测区域
    public RectTransform rightZone;     // 右侧检测区域
    public Camera mainCamera;           // 主摄像机
    public float scrollSpeed = 5f;      // 控制摄像机移动的速度
    private float minX = 0f;             // x轴最小值限制
    //private float maxX = 10f;

    void Update()
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        // 检测鼠标是否在左侧区域
        if (RectTransformUtility.RectangleContainsScreenPoint(leftZone, Input.mousePosition))
        {
            cameraPosition.x -= scrollSpeed * Time.deltaTime;
            cameraPosition.x = Mathf.Max(cameraPosition.x, minX); // 限制x不小于0
        }
        // 检测鼠标是否在右侧区域
        else if (RectTransformUtility.RectangleContainsScreenPoint(rightZone, Input.mousePosition))
        {
            // 向右移动摄像头
            cameraPosition.x += scrollSpeed * Time.deltaTime;
            //cameraPosition.x = Mathf.Min(cameraPosition.x, maxX);
        }
        
        // 更新摄像头的位置
        mainCamera.transform.position = cameraPosition;
    }
}
