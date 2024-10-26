using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LaserConverter : BaseLaserInstrument
{
    private List<LaserControl> lasers;

    private CircleCollider2D circleCollider;

    private bool isLeft = true;

    public bool canRotate = true;

    public override void OnLaserHit(LaserControl laser)
    {
        base.OnLaserHit(laser);

        ConvertLasers();

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
        base.OnAwake();
        isLaserStart = true;
        isLaserEnd = true;
        isRotatable = canRotate;
        isMovable = true;

        lasers = new List<LaserControl>();
        circleCollider = GetComponent<CircleCollider2D>();
    }

    protected override void OnFrame()
    {
        base.OnFrame();
        CheckHitLasers();
        if (HitLasers.Count <= 0)
        {
            foreach (var laser in lasers) laser.gameObject.SetActive(false);
        }
    }

    protected override void OnStart()
    {
        base.OnStart();
    }

    private void ConvertLasers()
    {
        while(lasers.Count < HitLasers.Count)
        {
            LaserControl laser = Instantiate(Resources.Load<GameObject>("Prefabs/Laser/Laser"), transform).GetComponent<LaserControl>();
            lasers.Add(laser);
            lasers[lasers.Count - 1].buildType = BuildType.Converter;

            LaserManager.Instance.Register(laser, new LaserInfo()
            {
                color = laserColor,
                isLaunch = false,
                isHit = false
            });
        }
        int i = 0;
        foreach (var item in HitLasers)
        {
            lasers[i].gameObject.SetActive(true);
            lasers[i].UpdatePosition(GetLaunchPosition(item.lastHitInfo.hitPoint, item.lastHitInfo.launchDirection, 
                                    transform.position, circleCollider.radius), item.lastHitInfo.launchDirection);
            lasers[i].Color = ConvertColor(item.Color);
            lasers[i].SetColor(lasers[i].Color);
            
            i++;
        }
        while (i < lasers.Count)
        {
            lasers[i].gameObject.SetActive(false);
            i++;
        }
;
    }

    private Vector2 GetLaunchPosition(Vector2 origin, Vector2 direction, Vector2 center, float radius)
    {
        // Normalize the direction
        direction.Normalize();

        // Calculate the point P on the line closest to the circle's center
        Vector2 OC = center - origin;
        float t = Vector2.Dot(OC, direction);
        Vector2 P = origin + t * direction;

        // Calculate the distance from P to the circle's edge
        float OP = (P - center).magnitude;
        float d = Mathf.Sqrt(radius * radius - OP * OP);

        // Calculate the exit point
        Vector2 exitPoint = P + d * direction;
        if (float.IsNaN(exitPoint.x) || float.IsNaN(exitPoint.y)) return origin + direction * 0.1f;
        else  return exitPoint + direction * 0.1f;
    }

    private LaserColor ConvertColor(LaserColor color)
    {
        Color _color = GetColor(color);
        if (isLeft)
        {
            float a = _color.r;
            _color.r = _color.g;
            _color.g = _color.b;
            _color.b = a;
        }
        else
        {
            float a = _color.b;
            _color.b = _color.g;
            _color.g = _color.r;
            _color.r = a;
        }

        return JudgeColor(_color);
    }

    private LaserColor JudgeColor(Color color)
    {
        if (color == Color.red) return LaserColor.Red;
        if (color == Color.green) return LaserColor.Green;
        if (color == Color.blue) return LaserColor.Blue;
        if (color == new Color(1, 1, 0, 1)) return LaserColor.Yellow;
        if (color == Color.cyan) return LaserColor.Cyan;
        if (color == Color.magenta) return LaserColor.Violet;
        if (color == Color.white) return LaserColor.White;

        return LaserColor.White;
    }


    protected override void RotateMethod(float scrollInput)
    {
        isLeft = !isLeft;
        if (isLeft) transform.rotation = Quaternion.Euler(0, 0, 0);
        else transform.rotation = Quaternion.Euler(0, 0, 180);
    }
}
