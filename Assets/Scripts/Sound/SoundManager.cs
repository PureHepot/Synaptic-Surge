using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ����������
/// </summary>
public class SoundManager
{
    private List<AudioSource> bgmSources;//����bgm����Ƶ���

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
                foreach (AudioSource source in bgmSources)
                {
                    source.Pause();
                }
            }
            else
            {
                foreach (AudioSource source in bgmSources)
                {
                    source.Play();
                }
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
            foreach(AudioSource source in bgmSources)
            {
                source.volume = bgmVolume;
            }
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
        bgmSources = new List<AudioSource>() { GameObject.Find("game").GetComponent<AudioSource>() };
        bgmSources[0].playOnAwake = false;
        isStop = false;
        bgmVolume = 1;
        effectVolume = 1;
    }

    //����bgm
    public void PlayBGM(string name, bool isLoop)
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
        AudioSource audioSource = null;
        for(int i = 0; i<bgmSources.Count; i++)
        {
            if (bgmSources[i].isPlaying == false) 
                audioSource = bgmSources[i];
        }
        if (audioSource == null)
        {
            audioSource = GameObject.Find("game").AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            bgmSources.Add(audioSource);
        }
        audioSource.loop = isLoop;
        audioSource.clip = clips[name];
        audioSource.Play();
    }


    public void StopBgm(string name)
    {
        foreach (var item in bgmSources)
        {
            if (item.clip == clips[name] && item.isPlaying)
            {
                item.Stop();
                return;
            }
        }
    }

    public bool IsPlaying(string name)
    {
        if (!clips.ContainsKey(name))
            return false;
        foreach (var item in bgmSources)
        {
            if(item.clip == clips[name] && item.isPlaying)
                return true;
        }

        return false;
    }

    public AudioSource GetAudioSourceByName(string name)
    {
        foreach (var item in bgmSources)
        {
            if (item.clip == clips[name])
                return item;
        }
        return null;
    }

    public void SetBgmVolume(string name, float val)
    {
        AudioSource source = GetAudioSourceByName(name);
        if (source != null && source.isPlaying)
        {
            source.volume = val;
        }
    }

}
