using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        LevelGate gate = null;
        int num = 0;
        foreach (var item in levelGateList)
        {
            if (levelGateMap[item])
                gate = item;
        }
        if(gate != null)
            return gate.level;
        else return num;
    }

    public void ChangeState(LevelGate gate, bool state)
    {
        if (levelGateMap.ContainsKey(gate))
        {
            levelGateMap[gate] = state;
        }
    }

    public void LoadHitedScene()
    {
        int level = GetLevel();
        LevelLoader.Instance.LoadNextLevel($"Level{level}");
    }
}
