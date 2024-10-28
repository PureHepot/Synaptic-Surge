using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGateManager : MonoBehaviour
{
    private List<LevelGate> levelGateList = new List<LevelGate>();
    private Dictionary<LevelGate, bool> levelGateMap = new Dictionary<LevelGate, bool>();

    public static LevelGateManager instance;

    public bool isStart = true;

    public GameObject _camera;
    private bool once = true;

    private void Awake()
    {
        instance = this;
        if (!GameApp.SoundManager.IsPlaying(Defines.LbBackground))
        {
            GameApp.SoundManager.PlayBGM(Defines.LbBackground, true);
            GameApp.SoundManager.SetBgmVolume(Defines.LbBackground, 0.5f);
        }
        StartCoroutine(makeFalse());
    }

    private void Start()
    {
        
    }


    private void Update()
    {
        if (once)
        {
            foreach (LevelGate levelGate in levelGateList)
            {
                if (levelGate.level == GameScene.gameData.level)
                {
                    _camera.transform.DOMoveX(levelGate.transform.position.x, 1.5f);
                    //_camera.transform.position += new Vector3(levelGate.transform.position.x - _camera.transform.position.x, _camera.transform.position.y, _camera.transform.position.z);
                }
            }
            once = false;   
        }

    }

    IEnumerator makeFalse()
    {
        if(GameScene.gameData.level != 1)
            GameApp.SoundManager.PlayBGM(Defines.DoorOpen, false);
        yield return new WaitForSeconds(0.1f);
        isStart = false;
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
