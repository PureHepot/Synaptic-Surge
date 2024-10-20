using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollController : MonoBehaviour
{
    public RectTransform leftZone;      // ���������
    public RectTransform rightZone;     // �Ҳ�������
    public Camera mainCamera;           // �������
    public float scrollSpeed = 5f;      // ����������ƶ����ٶ�
    private float minX = 0f;             // x����Сֵ����
    //private float maxX = 10f;

    void Update()
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        // �������Ƿ����������
        if (RectTransformUtility.RectangleContainsScreenPoint(leftZone, Input.mousePosition))
        {
            cameraPosition.x -= scrollSpeed * Time.deltaTime;
            cameraPosition.x = Mathf.Max(cameraPosition.x, minX); // ����x��С��0
        }
        // �������Ƿ����Ҳ�����
        else if (RectTransformUtility.RectangleContainsScreenPoint(rightZone, Input.mousePosition))
        {
            // �����ƶ�����ͷ
            cameraPosition.x += scrollSpeed * Time.deltaTime;
            //cameraPosition.x = Mathf.Min(cameraPosition.x, maxX);
        }
        
        // ��������ͷ��λ��
        mainCamera.transform.position = cameraPosition;
    }
}
