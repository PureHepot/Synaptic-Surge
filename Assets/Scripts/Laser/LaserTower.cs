using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : BaseLaserInstrument
{
    //���ⷢ�������
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
        //���ɼ���
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
