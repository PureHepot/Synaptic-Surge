using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LevelGate : BaseLaserInstrument
{
    public bool isPassed;
    public bool isHited;
    public int level;
    private bool isOpen = false;

    private Vector3 startPosition;
    public Vector3 endPosition;

    private Vector3 velocity;

    public override void OnLaserHit(LaserControl laser)
    {
        laser.IsStop = true;

        isHited = true;
        LevelGateManager.instance.Register(this, isHited);
    }

    protected override void OnAwake()
    {
        isLaserStart = false;
        isLaserEnd = true;
        isMovable = false;
        isRotatable = false;
    }

    
    protected override void OnStart()
    {
        LevelGateManager.instance.Register(this, isHited);

        startPosition = transform.position;
    }

    private void OnMouseDown()
    {
        if (isPassed)
        {
            isOpen = !isOpen;
        }
    }

    private float counter;
    public float speed;

    protected override void OnFrame()
    {
        base.OnFrame();
        counter += Time.deltaTime;
        if (counter > 0.1f)
        {
            counter = 0f;
            if (hitLaser != null && LaserManager.Instance.Check(hitLaser, this) == false)
            {
                isHited = false;
                LevelGateManager.instance.Register(this, isHited);
            }
        }

        if(isOpen)
        {
            transform.position = Vector3.Lerp(transform.position, endPosition, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, startPosition, speed * Time.deltaTime);
        }
    }
}
