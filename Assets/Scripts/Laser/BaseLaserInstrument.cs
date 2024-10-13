using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseLaserInstrument : MonoBehaviour
{
    //激光特性
    public LaserControl laser;
    public LaserControl hitLaser;
    protected LaserColor laserColor = LaserColor.White;


    //仪器特性
    protected bool isLaserStart = false;
    protected bool isLaserEnd = false;

    private void Awake()
    {
        OnAwake();
    }

    private void Start()
    {
        OnStart();
    }


    protected virtual void OnAwake()
    {

    }

    protected virtual void OnStart()
    {

    }

    //被激光击中效果
    public virtual void OnLaserHit()
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
        laser.gameObject.SetActive(false);
    }

    public virtual void ResetLaser()
    {

    }
}
