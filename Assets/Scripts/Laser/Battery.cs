using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Battery : BaseLaserInstrument
{
    private SpriteRenderer signRenderer;
    public GameObject poweredObj;
    public LaserColor powerColor;

    public bool connect2Build = true;
    public UnityEvent powerOnEvent;
    public UnityEvent powerOffEvent;

    private bool isPlayingEffect = false;
    private bool CanCharge = false;

    //需要的照射时间
    public float batteryEnergy = 4f;
    public float gatherSpd = 1;
    private float currentEnergy;

    public override void OnLaserHit(LaserControl laser)
    {
        if(laser.Color == powerColor)
            CanCharge = true;

        if(!isPlayingEffect && CanCharge)
        {
            isPlayingEffect = true;
            GameApp.SoundManager.PlayBGM(Defines.Charging, false);
        }


        laser.IsStop = true;
        if(CanCharge && currentEnergy >= batteryEnergy)
        {
            if(!isPowered)
                PowerOn();
        }
            
    }

    public override void PowerOn()
    {
        isPowered = true;
        if(connect2Build)
            poweredObj.GetComponent<BaseLaserInstrument>().PowerOn();
        else
        {
            powerOnEvent?.Invoke();
        }
    }
    public override void PowerOff()
    {
        isPowered = false;
        if (connect2Build)
            poweredObj.GetComponent<BaseLaserInstrument>().PowerOff();
        else
        {
             powerOffEvent?.Invoke();
        }
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
        isMovable = false;
        isRotatable = false;

        signRenderer = transform.Find("Sign").GetComponent<SpriteRenderer>();
    }

    protected override void OnStart()
    {
        base.OnStart();

        signRenderer.color = LaserControl.laserColors[powerColor];
    }

    private void Update()
    {
        if(CanCharge)
        {
            currentEnergy = Mathf.Clamp(currentEnergy + gatherSpd * Time.deltaTime, 0, batteryEnergy);
        }
        else
        {

            currentEnergy = Mathf.Clamp(currentEnergy - 2 * gatherSpd * Time.deltaTime, 0, batteryEnergy);
        }


        if (hitLaser != null && LaserManager.Instance.Check(hitLaser, this) == false)
        {
            ResetPowerSys();
        }

        

    }

    public override void ResetPowerSys()
    {
        if(isPlayingEffect)
        {
            isPlayingEffect = false;
            GameApp.SoundManager.StopBgm(Defines.Charging);
        }

        CanCharge = false;
        PowerOff();
    }
}
