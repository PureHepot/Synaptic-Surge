using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLight : BaseLaserInstrument
{
    private Transform _light;

    public bool isPowerOn{ get; private set; }

    public override void OnLaserHit(LaserControl laser)
    {
    }

    public override void PowerOff()
    {
        _light.GetComponent<SpriteRenderer>().color = Color.black;
        isPowerOn = false;
    }

    public override void PowerOn()
    {
        _light.GetComponent<SpriteRenderer>().color = Color.white;
        isPowerOn = true;
        GameApp.SoundManager.PlayBGM(Defines.Light, false);
    }

    public override void ResetLaser()
    {
    }

    public override void ResetPowerSys()
    {

    }

    protected override void LaserInit(Transform transform)
    {

    }

    protected override void OnAwake()
    {
        _light = transform.Find("Body/Head");
    }

    protected override void OnStart()
    {

    }
}
