using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class LaserInfo
{
    public List<BaseLaserInstrument> laserInstruments = new List<BaseLaserInstrument>();
    public LaserColor color;
    public bool isLaunch;
    public bool isHit;
}

public class LaserManager : MonoBehaviour
{
    private Dictionary<LaserControl, LaserInfo> valuePairs = new Dictionary<LaserControl, LaserInfo>();
    private float counter;
    public static LaserManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        
    }


    public void Register(LaserControl laserControl, LaserInfo info)
    {
        if (!valuePairs.ContainsKey(laserControl))
        {
            valuePairs.Add(laserControl, info);
        }
    }

    public void ChangeLaunchState(LaserControl laser, bool val)
    {
        if(valuePairs.ContainsKey(laser))
            valuePairs[laser].isLaunch = val;
    }

    public void ChangeHitState(LaserControl laser, bool val)
    {
        if (valuePairs.ContainsKey(laser))
            valuePairs[laser].isHit = val;
    }
    public void ChangeObjectState(LaserControl laser)
    {
        if (valuePairs.ContainsKey(laser))
        {
            valuePairs[laser].laserInstruments.Clear();
            foreach (HitInfo item in laser.hitInfoes)
            {
                valuePairs[laser].laserInstruments.Add(item.hitObject);
            }
        }
    }


    public bool Check(LaserControl laser, BaseLaserInstrument laserInstrument)
    {
        if (valuePairs.ContainsKey(laser))
        {
            if (valuePairs[laser].isHit == false)
            {
                return false;
            }

            foreach (BaseLaserInstrument item in valuePairs[laser].laserInstruments)
            {
                if (item == laserInstrument)
                    return true;
            }

        }
        return false;
    }
}
