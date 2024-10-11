using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaserMirror : BaseLaserInstrument
{
    //·´Éä¾µµ¯Éä·½Ïò
    Vector2 newDirection;

    private void Awake()
    {
        isLaserStart = false;
        isLaserEnd = false;
    }

    private void Start()
    {
        LaserInit(transform);
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

    public override void OnLaserHit()
    {
        laser.gameObject.SetActive(true);
        laser.HideVFX(0);

        newDirection = Vector2.Reflect(hitLaser.hitInfo.hitPoint, hitLaser.hitInfo.hitSurfaceNormal);
        laser.UpdatePosition(hitLaser.hitInfo.hitPoint, newDirection);

    }

    public override void ResetLaser()
    {
        laser.gameObject.SetActive(false);
    }

}
