using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LevelGate : BaseLaserInstrument
{
    public bool isPassed;
    public bool isHited;
    public int level;
    public bool isUp;
    private bool isOpen = false;

    private Vector3 startPosition;
    private Vector3 endPosition;

    public float distance;


    private SpriteRenderer point;

    public override void OnLaserHit(LaserControl laser)
    {
        laser.IsStop = true;

        isHited = true;
        LevelGateManager.instance.ChangeState(this, isHited);
    }

    protected override void OnAwake()
    {
        isLaserStart = false;
        isLaserEnd = true;
        isMovable = false;
        isRotatable = false;

        point = transform.Find("Point").GetComponent<SpriteRenderer>();
    }

    
    protected override void OnStart()
    {
        LevelGateManager.instance.Register(this, isHited);

        startPosition = transform.position;
        if (isUp)
            endPosition = startPosition + Vector3.up * distance;
        else
            endPosition = startPosition + Vector3.down * distance;




        if (GameScene.gameData.level > level)
        {
            isOpen = true;
            isPassed = true;
        }
        if (isOpen) point.color = Color.green;
        else point.color = Color.red;
    }

    private void OnMouseDown()
    {
        if (isPassed)
        {
            GameApp.SoundManager.PlayBGM(Defines.DoorOpen, false);
            isOpen = !isOpen;
            if (isOpen) point.color = Color.green;
            else point.color = Color.red;
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
                LevelGateManager.instance.ChangeState(this, isHited);
            }
        }

        if(isOpen && level == GameManager.lastPassLevel)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
        }
        else if(isOpen && LevelGateManager.instance.isStart)
        {
            transform.position = endPosition;
        }
        else if(isOpen)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPosition, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
        }
    }
}
