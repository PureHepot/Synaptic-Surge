using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserOther : BaseLaserInstrument
{
    private LaserColor hitLaserColor;
    public LaserColor HitLaserColor
    {
        get { return hitLaserColor; } 
        set { hitLaserColor = value; }
    }

    public override void OnLaserHit(LaserControl laser)
    {
        laser.IsStop = true;
        hitLaser = laser;
        hitLaserColor = laser.Color;

        transform.parent.GetComponent<LaserSynthesizer>().MixLaser();
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
    protected override void OnFrame()
    {
        counter += Time.deltaTime;
        if (counter > 0.1f)
        {
            counter = 0f;
            if (hitLaser != null && LaserManager.Instance.Check(hitLaser, this) == false)
            {
                hitLaser = null;
            }
        }
    }

    public bool CheckHitLaser()
    {
        return hitLaser != null && LaserManager.Instance.Check(hitLaser, this);
    }

}
