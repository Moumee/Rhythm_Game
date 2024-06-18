using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    SoundSO soundSO;
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioSource effectSource;
    public AudioSource stageSource;

    public enum SFX
    {
        Click,
        Start,
        Crack1,
        Crack2,
        Crack3,
        Bell,
        Success,
        SuccessEffect,
        Fail,

    }

    public enum BGM
    {
        MainMenu,
        Restaurant
    }

    public enum Stage
    {
        Hamster
    }

    private static AudioManager _Instance;
    public static AudioManager Instance
    {
        get
        {
            if (!_Instance)
            {
                var prefab = Resources.Load<GameObject>("AudioManagerPrefab");
                var inScene = Instantiate(prefab);
                _Instance = inScene.GetComponentInChildren<AudioManager>();
                if (!_Instance) _Instance = inScene.AddComponent<AudioManager>();
                DontDestroyOnLoad(_Instance.transform.root.gameObject);
            }
            return _Instance;
        }
    }

    private void Awake()
    {
        soundSO = Resources.Load<SoundSO>("SoundData");
    }


    public void PlayBGM(BGM bgm)
    {
        
        
        bgmSource.clip = GetBGMClip(bgm);
        bgmSource.Play();
        
    }
    public void PlaySFX(SFX sfx)
    {
        if (sfx == SFX.SuccessEffect)
        {
            effectSource.clip = GetSFXClip(sfx);
            effectSource.Play();    
        }
        else
        {
            sfxSource.clip = GetSFXClip(sfx);
            sfxSource.Play();
        }

    }

    public void PlayStageMusic(Stage stage)
    {


        stageSource.clip = GetStageClip(stage);
        stageSource.Play();

    }

    private AudioClip GetSFXClip(SFX sfx)
    {
        foreach (SoundSO.SFXAudioClip sfxAudioClip in soundSO.sfxAudioClipArray)
        {
            if (sfxAudioClip.sfx == sfx) return sfxAudioClip.audioClip;
        }
        Debug.LogError("Sound" + sfx + " not found!");
        return null;
    }

    private AudioClip GetBGMClip(BGM bgm)
    {
        foreach (SoundSO.BGMAudioClip bgmAudioClip in soundSO.bgmAudioClipArray)
        {
            if (bgmAudioClip.bgm == bgm) return bgmAudioClip.audioClip;
        }
        Debug.LogError("Sound" + bgm + " not found!");
        return null;
    }

    private AudioClip GetStageClip(Stage stage)
    {
        foreach (SoundSO.StageAudioClip stageAudioClip in soundSO.stageAudioClipArray)
        {
            if (stageAudioClip.stage == stage) return stageAudioClip.audioClip;
        }
        Debug.LogError("Sound" + stage + " not found!");
        return null;
    }
}
