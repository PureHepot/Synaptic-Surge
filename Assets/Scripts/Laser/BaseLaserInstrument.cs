using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BaseLaserInstrument : MonoBehaviour
{
    //��������
    public LaserControl laser;
    public LaserControl hitLaser;
    protected LaserColor laserColor = LaserColor.White;


    //��������
    protected bool isLaserStart = false;
    protected bool isLaserEnd = false;


    //���������Ч��
    public virtual void OnLaserHit()
    {

    }

    public bool IsLaserStop()
    {
        return isLaserEnd;
    }

    protected void LaserInit(Transform transform)
    {
        laser = Instantiate(Resources.Load<GameObject>("Prefabs/Laser/Laser"), transform).GetComponent<LaserControl>();
        laser.Color = laserColor;
        laser.gameObject.SetActive(false);
    }

    public virtual void ResetLaser()
    {

    }
}
