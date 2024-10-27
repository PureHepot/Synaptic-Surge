using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

struct LaserPair
{
    public LaserColor firstColor;
    public LaserColor secondColor;

    public LaserPair(LaserColor fColor, LaserColor sColor)
    {
        firstColor = fColor;
        secondColor = sColor;
    }
}

public class LaserSynthesizer : BaseLaserInstrument
{
    private List<Transform> points;
    private Transform gunPos;

    private LaserPair pair;

    public bool canRotate = true;


    public override void OnLaserHit(LaserControl laser)
    {
        base.OnLaserHit(laser);
        laser.IsStop = true;
    }

    public override void PowerOff()
    {
        base.PowerOff();
    }

    public override void PowerOn()
    {
        base.PowerOn();
    }

    public override void ResetLaser()
    {
        base.ResetLaser();
    }

    public override void ResetPowerSys()
    {
        base.ResetPowerSys();
    }

    protected override void LaserInit(Transform transform)
    {
        base.LaserInit(transform);
    }

    protected override void OnAwake()
    {
        isLaserStart = true;
        isLaserEnd = false;
        isRotatable = canRotate;
        isMovable = true;

        points = new List<Transform>();
        for (int i = 1; i <= 2; i++)
        {
            points.Add(transform.Find($"Point{i}"));
        }
        gunPos = transform.Find("GunPos");

    }

    protected override void OnStart()
    {
        LaserInit(gunPos);
        LaserManager.Instance.Register(laser, new LaserInfo()
        {
            isLaunch = true,
            isHit = true,
            color = laserColor
        });
    }

    private float counter;
    protected override void OnFrame()
    {
        base.OnFrame();
        counter += Time.deltaTime;
        if (counter > 0.1f)
        {
            counter = 0f;
            for (int i = 0; i < points.Count; i++)
                if (points[i].GetComponent<LaserOther>().CheckHitLaser() == false)
                {
                    ResetLaser();
                }
        }
    }


    public void MixLaser()
    {
        for (int i = 0; i < points.Count; i++)
        {
            if (points[i].GetComponent<LaserOther>().hitLaser == null)
            {
                ResetLaser();
                return;
            }
        }

        pair.firstColor = points[0].GetComponent<LaserOther>().HitLaserColor;
        pair.secondColor = points[1].GetComponent<LaserOther>().HitLaserColor;

        laser.Color = AddColors(pair.firstColor, pair.secondColor);
        laser.SetColor(laser.Color);
        laser.gameObject.SetActive(true);
    }


    

    public LaserColor AddColors(LaserColor color1, LaserColor color2)
    {
        Color resultColor = GetColor(color1) + GetColor(color2);

        if(resultColor.r>1) resultColor.r = 1;
        if(resultColor.g>1) resultColor.g = 1;
        if(resultColor.b>1) resultColor.b = 1;
        if(resultColor.a>1) resultColor.a = 1;

        // 判断结果颜色与枚举对应
        if (resultColor == Color.red) return LaserColor.Red;
        if (resultColor == Color.green) return LaserColor.Green;
        if (resultColor == Color.blue) return LaserColor.Blue;
        if (resultColor == new Color(1,1,0,1)) return LaserColor.Yellow;
        if (resultColor == Color.cyan) return LaserColor.Cyan;
        if (resultColor == Color.magenta) return LaserColor.Violet;
        if (resultColor == Color.white) return LaserColor.White;

        // 如果不精确匹配，返回白色（光叠加后的亮色）
        return LaserColor.White;
    }
}
