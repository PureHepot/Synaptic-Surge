using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmManager : MonoBehaviour
{
    public static BgmManager instance;
    private void Awake()
    {
        instance = this;
    }

    private List<AudioSource> bgmSources;//����bgm����Ƶ���

    private Dictionary<string, AudioClip> clips;//��Ƶ�����ֵ�

    //����bgm
    public void PlayBGM(string name)
    {
        //û�е�ǰ����Ƶ
        if (clips.ContainsKey(name) == false)
        {
            AudioClip clip = (AudioClip)Resources.Load<AudioClip>($"Sounds/{name}");
            clips.Add(name, clip);
        }
        AudioSource audioSource = null;
        for (int i = 0; i < bgmSources.Count; i++)
        {
            if (bgmSources[i].isPlaying == false)
                audioSource = bgmSources[i];
        }
        if (audioSource == null)
        {
            audioSource = GameObject.Find("game").AddComponent<AudioSource>();
            bgmSources.Add(audioSource);
        }
        audioSource.clip = clips[name];
        audioSource.Play();
    }
}
