using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
<<<<<<< Updated upstream
=======
using FMODUnity;
using FMOD.Studio;
>>>>>>> Stashed changes

public class AudioManager : MonoBehaviour
{
    SoundSO soundSO;
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioSource effectSource;
    public AudioSource stageSource;

<<<<<<< Updated upstream
=======
    public EventReference click;
    public EventReference start;
    public EventReference bell;
    public EventReference success;
    public EventReference successEffect;
    public EventReference fail;
    public EventReference crack;
    public EventReference chocolate;
    public EventReference mainMenu;
    public EventReference restaurant;
    public EventReference stage1;

    public EventInstance stageEventInstance;
    public EventInstance bgmEventInstance;



>>>>>>> Stashed changes
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
        Choco1,
        Choco2,
        Choco3

    }

    public enum BGM
    {
        MainMenu,
        Restaurant
    }

    public enum Stage
    {
        Hamster,
        Cat
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


    public void PlayBGM(EventReference bgm)
    {
<<<<<<< Updated upstream
        
        
        bgmSource.clip = GetBGMClip(bgm);
        bgmSource.Play();
=======
        bgmEventInstance = RuntimeManager.CreateInstance(bgm);
        bgmEventInstance.start();
        bgmEventInstance.release();
        //bgmSource.clip = GetBGMClip(bgm);
        //bgmSource.Play();
>>>>>>> Stashed changes
        
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

    public void PlayStageMusic(EventReference stageBGM)
    {
<<<<<<< Updated upstream

        stageSource.clip = null;
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
=======
        stageEventInstance = RuntimeManager.CreateInstance(stageBGM);
        stageEventInstance.start();
        stageEventInstance.release();
        //stageSource.clip = null;
        //stageSource.clip = GetStageClip(stage);
        //stageSource.Play();

    }

    

    

    //private EventReference GetSFXClip(SFX sfx)
    //{
    //    foreach (SoundSO.SFXAudioClip sfxAudioClip in soundSO.sfxAudioClipArray)
    //    {
    //        if (sfxAudioClip.sfx == sfx) return sfxAudioClip.sfxSound;
    //    }
    //    return null;
    //}
>>>>>>> Stashed changes

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
