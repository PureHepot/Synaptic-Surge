using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerLaserTower : BaseLaserInstrument
{
    private Transform gunPos;

    public override void OnLaserHit(LaserControl laser)
    {
        base.OnLaserHit(laser);
        if (isHitedbyLaser && laser.buildType < GetHighPriorityLaser()) return;
        isHitedbyLaser = true;

        this.laser.gameObject.SetActive(true);
        this.laser.Color = laser.Color;
        this.laser.SetColor(laser.Color);
    }

    protected override void OnAwake()
    {
        base.OnAwake();
        isLaserStart = true;
        isLaserEnd = true;
        isMovable = false;
        isRotatable = false;

        gunPos = transform.Find("GunPos");
    }

    protected override void OnFrame()
    {
        base.OnFrame();

        if (hitLaser != null && LaserManager.Instance.Check(hitLaser, this) == false)
        {
            isHitedbyLaser = false;
        }
    }

    protected override void OnStart()
    {
        base.OnStart();
        LaserInit(gunPos);
        laser.buildType = BuildType.TriggerTower;
        LaserManager.Instance.Register(laser, new LaserInfo()
        {
            color = laserColor,
            isLaunch = true,
            isHit = false
        });
    }
}
