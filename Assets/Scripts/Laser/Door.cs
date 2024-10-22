using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : BaseLaserInstrument
{
    private Transform leftDoor;
    private Transform rightDoor;

    public List<Vector3> poses;
    private List<Vector3> originPos = new List<Vector3>();

    public float speed;

    private bool isOpen;


    public override void OnLaserHit(LaserControl laser)
    {
        laser.IsStop = true;
    }

    public override void PowerOff()
    {
        isOpen = false;
    }

    public override void PowerOn()
    {
        isOpen = true;
    }

    protected override void OnAwake()
    {
        isLaserEnd = true;

        leftDoor = transform.Find("Left").transform;
        rightDoor = transform.Find("Right").transform;

        originPos.Add(leftDoor.position);
        originPos.Add(rightDoor.position);
    }

    protected override void OnFrame()
    {
        if (isOpen)
        {
            leftDoor.position = Vector3.MoveTowards(leftDoor.position, poses[0], speed * Time.deltaTime);
            rightDoor.position = Vector3.MoveTowards(rightDoor.position, poses[1], speed * Time.deltaTime);
        }
        else
        {
            leftDoor.position = Vector3.MoveTowards(leftDoor.position, originPos[0], speed * Time.deltaTime);
            rightDoor.position = Vector3.MoveTowards(rightDoor.position, originPos[1], speed * Time.deltaTime);
        }
    }

    protected override void OnStart()
    {
        base.OnStart();
    }
}
