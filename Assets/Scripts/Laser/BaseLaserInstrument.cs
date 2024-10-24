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
    protected List<LaserControl> HitLasers = new List<LaserControl>();
    public LaserColor laserColor = LaserColor.White;


    //仪器特性
    protected bool isHitedbyLaser = false;
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
        rotateAngle = transform.rotation.eulerAngles.z;
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
        laser.IsStop = isLaserEnd;
        if(!HitLasers.Contains(laser))
            HitLasers.Add(laser);
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
        isHitedbyLaser = false;

        LaserManager.Instance.ChangeHitState(laser, false);
        LaserManager.Instance.ChangeLaunchState(laser, false);
        laser.gameObject.SetActive(false);
    }

    public virtual void ResetPowerSys()
    {

    }

    protected BuildType GetHighPriorityLaser()
    {
        LaserControl laser = HitLasers[0];
        foreach(LaserControl lazer in HitLasers)
        {
            if(lazer.buildType>laser.buildType)
                laser = lazer;
        }

        return laser.buildType;
    }

    protected void CheckHitLasers()
    {
        if (HitLasers.Count <= 0) return;
        for(int i = 0; i < HitLasers.Count; i++) 
        {
            if (LaserManager.Instance.Check(HitLasers[i], this) == false)
            {
                HitLasers.Remove(HitLasers[i]);
            }
        }
    }
}
