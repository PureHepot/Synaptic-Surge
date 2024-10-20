using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGateManager : MonoBehaviour
{
    private List<LevelGate> levelGateList = new List<LevelGate>();
    private Dictionary<LevelGate, bool> levelGateMap = new Dictionary<LevelGate, bool>();

    public static LevelGateManager instance;

    private void Awake()
    {
        instance = this;
    }


    public void Register(LevelGate gate, bool isHited)
    {
        if (!levelGateMap.ContainsKey(gate))
        {
            levelGateList.Add(gate);
            levelGateMap.Add(gate, isHited);
        }
    }

    public int GetLevel()
    {
        LevelGate gate = new LevelGate();
        foreach (var item in levelGateList)
        {
            if (levelGateMap[item])
                gate = item;
        }
        return gate.level;
    }
}
