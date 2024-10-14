using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRefractor : BaseLaserInstrument
{
    private Transform gunPos;


    protected override void OnAwake()
    {
        isLaserStart = true;
        isLaserEnd = true;
        isRotatable = true;
        gunPos = transform.Find("GunPos");
    }

    protected override void OnStart()
    {
        LaserInit(gunPos);
        LaserManager.Instance.Register(laser, new LaserInfo()
        { 
            color = laserColor,
            isLaunch = false,
            isHit = false
        });
    }

    private float counter;
    protected override void OnFrame()
    {
        base.OnFrame();
        counter += Time.deltaTime;
        if (counter > 0.1f)
        {
            counter = 0f;
            if (hitLaser != null && LaserManager.Instance.Check(hitLaser, this) == false)
            {
                ResetLaser();
            }
        }
    }


    protected override void LaserInit(Transform transform)
    {
        base.LaserInit(transform);
    }


    public override void OnLaserHit(LaserControl lazer)
    {
        lazer.IsStop = true;

        LaserManager.Instance.ChangeLaunchState(laser, true);
        laser.gameObject.SetActive(true);
        laser.Color = lazer.Color;
        laser.SetColor(laser.Color);
    }

    public override void ResetLaser()
    {
        LaserManager.Instance.ChangeHitState(laser, false);
        LaserManager.Instance.ChangeLaunchState(laser, false);
        laser.gameObject.SetActive(false);
    }
}
