using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����������
/// </summary>
public class SoundManager
{
    private AudioSource bgmSource;//����bgm����Ƶ���

    private Dictionary<string, AudioClip> clips;//��Ƶ�����ֵ�

    private bool isStop;//�Ƿ���

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

    private float bgmVolume;//bgm������С

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

    private float effectVolume;//��Ч��С

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

    //����bgm
    public void PlayBGM(string name)
    {
        if(isStop)
        {
            return;
        }

        //û�е�ǰ����Ƶ
        if(clips.ContainsKey(name) == false)
        {
            AudioClip clip = (AudioClip)Resources.Load<AudioClip>($"Sounds/{name}");
            clips.Add(name,clip);
        }
        bgmSource.clip = clips[name];
        bgmSource.Play();
    }
}
