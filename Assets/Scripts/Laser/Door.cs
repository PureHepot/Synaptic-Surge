using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : BaseLaserInstrument
{
    private Transform leftDoor;
    private Transform rightDoor;

    public float distance;
    private List<Vector3> originPos = new List<Vector3>();

    public float speed;

    public bool isHorizon = true;
    public bool isInverse;

    private bool isOpen;

    private Vector3 newPositionL;
    private Vector3 newPositionR;

    public Color doorColor;


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

        leftDoor.GetComponent<SpriteRenderer>().color = doorColor;
        rightDoor.GetComponent<SpriteRenderer>().color = doorColor;

        originPos.Add(leftDoor.position);
        originPos.Add(rightDoor.position);

        newPositionL = isHorizon ? leftDoor.position + Vector3.left*distance : leftDoor.position + Vector3.up*distance;
        newPositionR = isHorizon ? rightDoor.position + Vector3.right*distance : rightDoor.position + Vector3.down*distance;

        if(isInverse)
        {
            leftDoor.position = newPositionL;
            rightDoor.position = newPositionR;
        }
    }

    protected override void OnFrame()
    {
        if (isOpen)
        {
            if (isInverse)
            {
                leftDoor.position = Vector3.MoveTowards(leftDoor.position, originPos[0], speed * Time.deltaTime);
                rightDoor.position = Vector3.MoveTowards(rightDoor.position, originPos[1], speed * Time.deltaTime);
            }
            else
            {
                leftDoor.position = Vector3.MoveTowards(leftDoor.position, newPositionL, speed * Time.deltaTime);
                rightDoor.position = Vector3.MoveTowards(rightDoor.position, newPositionR, speed * Time.deltaTime);
            }
        }
        else
        {
            if (isInverse)
            {
                leftDoor.position = Vector3.MoveTowards(leftDoor.position, newPositionL, speed * Time.deltaTime);
                rightDoor.position = Vector3.MoveTowards(rightDoor.position, newPositionR, speed * Time.deltaTime);
            }
            else
            {
                leftDoor.position = Vector3.MoveTowards(leftDoor.position, originPos[0], speed * Time.deltaTime);
                rightDoor.position = Vector3.MoveTowards(rightDoor.position, originPos[1], speed * Time.deltaTime);
            }
        }
    }

    protected override void OnStart()
    {
        base.OnStart();
    }
}
