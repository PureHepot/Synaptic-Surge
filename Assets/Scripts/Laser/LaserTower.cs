using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : BaseLaserInstrument
{
    //激光发射器相关
    private Transform gunPos;
    private Vector3 laserDirection;


    protected override void OnAwake()
    {
        isLaserStart = true;
        isLaserEnd = false;
        gunPos = transform.Find("GunPos");
        laserDirection = (gunPos.localPosition - transform.position).normalized;
    }

    protected override void OnStart()
    {
        //生成激光
        LaserInit(gunPos);
        laser.gameObject.SetActive(true);
        LaserManager.Instance.Register(laser, new LaserInfo() {
            color = laserColor,
            isLaunch = true,
            isHit = false
        });
    }

    private void Update()
    {
        
    }
}
