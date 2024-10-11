using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserInfo
{
    public BaseLaserInstrument laserInstrument;
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
        counter += Time.deltaTime;
        if(counter >= 0.1f)
        {
            foreach(var pair in valuePairs)
            {
                if (pair.Value.isHit)
                {
                    pair.Value.laserInstrument.OnLaserHit();
                }
            }
            counter = 0;
        }
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
    public void ChangeObjectState(LaserControl laser, BaseLaserInstrument val)
    {
        if (valuePairs.ContainsKey(laser))
            valuePairs[laser].laserInstrument = val;
    }


    public bool Check(LaserControl laser, BaseLaserInstrument laserInstrument)
    {
        if (valuePairs.ContainsKey(laser))
        {
            if (valuePairs[laser].isHit == false)
            {
                return false;
            }

            if (laserInstrument != valuePairs[laser].laserInstrument)
                return false;

            return true;
        }
        return false;
    }
}
