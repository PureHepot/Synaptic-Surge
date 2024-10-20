using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class BaseLaserInstrument : MonoBehaviour
{
    //激光特性
    protected LaserControl laser;
    public LaserControl hitLaser;
    public LaserColor laserColor = LaserColor.White;


    //仪器特性
    protected bool isLaserStart = false;
    protected bool isLaserEnd = false;
    protected bool isPowered = false;
    protected bool isRotatable = false;
    private bool isMouseOver = false;
    public bool isMovable = false;
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
        isMouseOver = true; // 鼠标进入物体
    }

    protected void OnMouseExit()
    {
        isMouseOver = false; // 鼠标离开物体
    }

    protected void OnMouseDrag()
    {
        if (isMovable)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 moveVec = (cursorPos - (Vector2)transform.position) * 10;
            if (moveVec.magnitude < 2f) moveVec = moveVec.normalized * 2;
            GetComponent<Rigidbody2D>().velocity = moveVec;
        }
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

    //被激光击中效果
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
        LaserManager.Instance.ChangeHitState(laser, false);
        LaserManager.Instance.ChangeLaunchState(laser, false);
        laser.gameObject.SetActive(false);
    }

    public virtual void ResetPowerSys()
    {

    }
}
