using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BaseLaserInstrument : MonoBehaviour
{
    //��������
    protected LaserControl laser;
    public LaserControl hitLaser;
    public LaserColor laserColor = LaserColor.White;


    //��������
    protected bool isLaserStart = false;
    protected bool isLaserEnd = false;
    protected bool isPowered = false;
    protected bool isRotatable = false;
    private bool isMouseOver = false;
    public bool IsPowered
    {
        get { return isPowered; }
        set { isPowered = value; }
    }


    protected float rotateAngle = 0;
    

    private void Awake()
    {
        OnAwake();
    }

    private void Start()
    {
        OnStart();
    }

    private void Update()
    {
        OnFrame();
    }

    protected void OnMouseEnter()
    {
        isMouseOver = true; // ����������
    }

    protected void OnMouseExit()
    {
        isMouseOver = false; // ����뿪����
    }


    protected virtual void OnAwake()
    {

    }

    protected virtual void OnStart()
    {

    }

    protected virtual void OnFrame()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (isMouseOver)
            if (scrollInput != 0 && isRotatable)
            {
                Quaternion targetRotation = Quaternion.Euler(0, 0, rotateAngle += scrollInput * 40);

                transform.rotation = targetRotation;
            }
    }

    //���������Ч��
    public virtual void OnLaserHit()
    {

    }

    public virtual void PowerOn()
    {

    }

    public virtual void PowerOff()
    {

    }

    public virtual void OnLaserHit(LaserControl laser)
    {

    }

    public bool IsLaserStop()
    {
        return isLaserEnd;
    }

    protected virtual void LaserInit(Transform transform)
    {
        laser = Instantiate(Resources.Load<GameObject>("Prefabs/Laser/Laser"), transform).GetComponent<LaserControl>();
        laser.Color = laserColor;
        laser.SetColor(laserColor);
        laser.gameObject.SetActive(false);
    }

    public virtual void ResetLaser()
    {
        laser.gameObject.SetActive(false);
    }

    public virtual void ResetPowerSys()
    {

    }
}
