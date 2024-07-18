using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    public SoundSO soundSO;
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioSource effectSource;
    public AudioSource stageSource;

    public EventReference click;
    public EventReference start;
    public EventReference bell;
    public EventReference success;
    public EventReference successEffect;
    public EventReference fail;
    public EventReference crack;
    public EventReference notePress;
    public EventReference chocolate;
    public EventReference mainMenu;
    public EventReference introVideoAudio;
    public EventReference restaurant;
    public EventReference stage1;

    public EventInstance bgmEventInstance;

    [Header("Volume")]
    [Range(0, 1)]
    public float masterVolume = 1;

    [Range(0, 1)]
    public float bgmVolume = 1;

    [Range(0, 1)]
    public float sfxVolume = 1;

    private Bus masterBus;
    private Bus bgmBus;
    private Bus sfxBus; 



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
        masterBus = RuntimeManager.GetBus("bus:/");
        bgmBus = RuntimeManager.GetBus("bus:/BGM");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");
    }

    private void Update()
    {
        masterBus.setVolume(masterVolume);
        bgmBus.setVolume(bgmVolume);
        sfxBus.setVolume(sfxVolume);    
    }

    public void StopAllMusic()
    {
        if (bgmEventInstance.isValid())
        {
            bgmEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            bgmEventInstance.release();
        }
        if (BeatTracker.currentMusicTrack.isValid())
        {
            BeatTracker.currentMusicTrack.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            BeatTracker.currentMusicTrack.release();
        }
    }

    public void PlayBGM(EventReference bgm)
    {
        StopAllMusic(); // Stop any existing music before playing new BGM
        bgmEventInstance = RuntimeManager.CreateInstance(bgm);
        bgmEventInstance.start();
    }

    public void PlaySFX(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound);
    }

    //public void PlayStageMusic(EventReference stageBGM)
    //{
    //    StopAllMusic(); // Stop any existing music before playing new stage music
    //    stageEventInstance = RuntimeManager.CreateInstance(stageBGM);
    //    stageEventInstance.start();
    //}

    // Removed commented-out methods for GetSFXClip, GetBGMClip, and GetStageClip
}