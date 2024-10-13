using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDiffuser : BaseLaserInstrument
{
    private LaserControl[] lasers = new LaserControl[3];

    private Transform[] gunPoses = new Transform[3];

    private float rotateAngle = 0;

    protected override void OnAwake()
    {
        isLaserStart = true;
        isLaserEnd = true;

        for(int i = 0; i < 3; i++)
        {
            gunPoses[i] = transform.Find($"GunPos{i+1}");
        }
    }

    protected override void OnStart()
    {
        for(int i = 0;i < 3;i++)
        {
            LaserInit(gunPoses[i], i);
            LaserManager.Instance.Register(lasers[i], new LaserInfo()
            {
                color = laserColor,
                isLaunch = false,
                isHit = false
            });
            lasers[i].UpdatePosition(gunPoses[i].position, gunPoses[i].position - transform.position);
        }
    }

    private void OnMouseDown()
    {
        Quaternion targetRotation = Quaternion.Euler(0, 0, rotateAngle -= 45);

        transform.rotation = targetRotation;
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


    public override void OnLaserHit(LaserControl laser)
    {
        laser.IsStop = true;
        for (int i = 0; i < 3; i++)
        {
            LaserManager.Instance.ChangeLaunchState(lasers[i], true);
            lasers[i].gameObject.SetActive(true);
        }
        switch (laser.Color)
        {
            case LaserColor.White:
                lasers[0].Color = LaserColor.Red;
                lasers[1].Color = LaserColor.Green;
                lasers[2].Color = LaserColor.Blue;
                break;

            case LaserColor.Yellow:
                lasers[0].Color = LaserColor.Red;
                lasers[1].Color = LaserColor.Yellow;
                lasers[2].Color = LaserColor.Green;
                break;
            case LaserColor.Cyan:
                lasers[0].Color = LaserColor.Green;
                lasers[1].Color = LaserColor.Cyan;
                lasers[2].Color = LaserColor.Blue;
                break;
            case LaserColor.Violet:
                lasers[0].Color = LaserColor.Blue;
                lasers[1].Color = LaserColor.Violet;
                lasers[2].Color = LaserColor.Red;
                break;
            case LaserColor.Red:
                lasers[0].Color = LaserColor.Red;
                lasers[1].Color = LaserColor.Red;
                lasers[2].Color = LaserColor.Red;
                break;
            case LaserColor.Blue:
                lasers[0].Color = LaserColor.Blue;
                lasers[1].Color = LaserColor.Blue;
                lasers[2].Color = LaserColor.Blue;
                break;
            case LaserColor.Green:
                lasers[0].Color = LaserColor.Green;
                lasers[1].Color = LaserColor.Green;
                lasers[2].Color = LaserColor.Green;
                break;
        }

        for (int i = 0; i < 3; i++)
        {
            lasers[i].SetColor(lasers[i].Color);
        }

    }

    public override void ResetLaser()
    {
        for (int i = 0; i < 3; i++)
        {
            lasers[i].gameObject.SetActive(false);
        }
    }

    private void LaserInit(Transform transform, int i)
    {
        lasers[i] = Instantiate(Resources.Load<GameObject>("Prefabs/Laser/Laser"), transform).GetComponent<LaserControl>();
        lasers[i].Color = laserColor;
        lasers[i].SetColor(laserColor);
        lasers[i].gameObject.SetActive(false);
    }
}
