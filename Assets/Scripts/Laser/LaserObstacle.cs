using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObstacle : BaseLaserInstrument
{
    public override void OnLaserHit()
    {

    }

    public override void OnLaserHit(LaserControl laser)
    {
        laser.IsStop = true;
    }

    public override void ResetLaser()
    {

    }

    protected override void LaserInit(Transform transform)
    {

    }

    protected override void OnAwake()
    {
        isLaserStart = false;
        isLaserEnd = true;
        isMovable = false;
    }

    protected override void OnStart()
    {
        base.OnStart();
    }
}
