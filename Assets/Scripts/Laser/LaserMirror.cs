using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class LaserMirror : BaseLaserInstrument
{
    //反射镜弹射方向
    Vector2 newDirection;

    protected override void OnAwake()
    {
        isLaserStart = false;
        isLaserEnd = false;
    }

    protected override void OnStart()
    {
        //LaserInit(transform);
        //LaserManager.Instance.Register(laser, new LaserInfo()
        //{
        //    color = laserColor,
        //    isLaunch = false,
        //    isHit = false
        //});
    }

    protected override void OnFrame()
    {
        
    }

    public override void OnLaserHit()
    {
        laser.gameObject.SetActive(true);
        laser.HideVFX(0);

        //newDirection = Vector2.Reflect(hitLaser.hitInfo.launchDirection, hitLaser.hitInfo.hitSurfaceNormal);
        //laser.UpdatePosition(hitLaser.hitInfo.hitPoint, newDirection);

    }

    public override void OnLaserHit(LaserControl laser)
    {
        laser.IsStop = false;

        newDirection = Vector2.Reflect(laser.hitInfoes[laser.HitCount-1].launchDirection, laser.hitInfoes[laser.HitCount - 1].hitSurfaceNormal);
        
        RaycastHit2D hit = Physics2D.Raycast(laser.hitInfoes[laser.HitCount - 1].hitPoint, newDirection);
        laser.RoadDFS(newDirection, hit);

        if (hit && hit.transform != this)
        {
            laser.HitCount++;//撞到了就记录

            BaseLaserInstrument laserInstrument = hit.transform.GetComponent<BaseLaserInstrument>();
            laserInstrument.hitLaser = laser;

            laserInstrument.OnLaserHit(laser);
        }
    }


}
