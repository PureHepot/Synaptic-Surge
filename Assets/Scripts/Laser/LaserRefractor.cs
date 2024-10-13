using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRefractor : BaseLaserInstrument
{
    private float rotateAngle = 0;
    private Transform gunPos;


    protected override void OnAwake()
    {
        isLaserStart = true;
        isLaserEnd = true;
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
    private void Update()
    {
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

    private void OnMouseDown()
    {
        Quaternion targetRotation = Quaternion.Euler(0, 0, rotateAngle += 45);

        transform.rotation = targetRotation;
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
    }

    public override void ResetLaser()
    {
        laser.gameObject.SetActive(false);
    }
}
