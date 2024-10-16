using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : BaseLaserInstrument
{
    public GameObject poweredObj;
    public LaserColor powerColor;

    private bool isOnceShot = true;

    //需要的照射时间
    public float requireTime = 1f;
    private float lastTime;
    private float addTime;

    public override void OnLaserHit(LaserControl laser)
    {
        if (isOnceShot)
        {
            isOnceShot = false;
            lastTime = Time.time;
        }
        else
        {
            addTime += Time.time - lastTime;
            lastTime = Time.time;
        }

        laser.IsStop = true;
        if(laser.Color == powerColor && addTime > requireTime)
        {
            addTime = 0;
            PowerOn();
        }
            
    }

    public override void PowerOn()
    {
        isPowered = true;
        poweredObj.GetComponent<BaseLaserInstrument>().PowerOn();
    }
    public override void PowerOff()
    {
        addTime = 0;
        isPowered = false;
        isOnceShot = true;
        poweredObj.GetComponent<BaseLaserInstrument>().PowerOff();
    }


    public override void ResetLaser()
    {
        base.ResetLaser();
    }

    protected override void LaserInit(Transform transform)
    {

    }

    protected override void OnAwake()
    {
        isLaserStart = false;
        isLaserEnd = true;
    }

    protected override void OnStart()
    {
        base.OnStart();
    }

    private float counter;
    private void Update()
    {
        counter += Time.deltaTime;
        if (counter > 0.1f)
        {
            counter = 0f;
            if (hitLaser != null && LaserManager.Instance.Check(hitLaser, this) == false)
            {
                ResetPowerSys();
            }
        }
    }

    public override void ResetPowerSys()
    {
        PowerOff();
    }
}
