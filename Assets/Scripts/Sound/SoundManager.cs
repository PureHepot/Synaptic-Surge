using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 声音管理器
/// </summary>
public class SoundManager
{
    private AudioSource bgmSource;//播放bgm的音频组件

    private Dictionary<string, AudioClip> clips;//音频缓存字典

    private bool isStop;//是否静音

    public bool IsStop
    {
        get
        {
            return isStop;
        }
        set
        {
            isStop = value;
            if(isStop == true)
            {
                bgmSource.Pause();
            }
            else
            {
                bgmSource.Play();
            }
        }
    }

    private float bgmVolume;//bgm音量大小

    public float BgmVolume
    {
        get
        {
            return  bgmVolume;
        }
        set
        {
            bgmVolume  = value; 
            bgmSource.volume = bgmVolume;
        }
    }

    private float effectVolume;//音效大小

    public float EffectVolume
    {
        get
        {
            return effectVolume;
        }
        set
        {
            effectVolume = value;
        }
    }

    public SoundManager()
    {
        clips = new Dictionary<string, AudioClip>();
        bgmSource = GameObject.Find("game").GetComponent<AudioSource>();

        isStop = false;
        bgmVolume = 1;
        effectVolume = 1;
    }

    //播放bgm
    public void PlayBGM(string name)
    {
        if(isStop)
        {
            return;
        }

        //没有当前的音频
        if(clips.ContainsKey(name) == false)
        {
            AudioClip clip = (AudioClip)Resources.Load<AudioClip>($"Sounds/{name}");
            clips.Add(name,clip);
        }
        bgmSource.clip = clips[name];
        bgmSource.Play();
    }
}
