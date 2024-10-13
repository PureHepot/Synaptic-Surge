using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : BaseLaserInstrument
{
    public GameObject poweredObj;
    public LaserColor powerColor;


    public override void OnLaserHit(LaserControl laser)
    {
        laser.IsStop = true;
        if(laser.Color == powerColor)
            PowerOn();
    }

    public override void PowerOn()
    {
        isPowered = true;
        poweredObj.GetComponent<BaseLaserInstrument>().PowerOn();
    }
    public override void PowerOff()
    {
        isPowered = false;
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
