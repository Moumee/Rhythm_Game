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
    public EventReference chocolate;
    public EventReference mainMenu;
    public EventReference restaurant;
    public EventReference stage1;

    public EventInstance stageEventInstance;
    public EventInstance bgmEventInstance;

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
        //soundSO = Resources.Load<SoundSO>("SoundData");
    }

    public void StopAllMusic()
    {
        if (bgmEventInstance.isValid())
        {
            bgmEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            bgmEventInstance.release();
        }
        if (stageEventInstance.isValid())
        {
            stageEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            stageEventInstance.release();
        }
    }

    public void PlayBGM(EventReference bgm)
    {
        StopAllMusic(); // Stop any existing music before playing new BGM
        bgmEventInstance = RuntimeManager.CreateInstance(bgm);
        bgmEventInstance.start();
        bgmEventInstance.release();
    }

    public void PlaySFX(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound);
    }

    public void PlayStageMusic(EventReference stageBGM)
    {
        StopAllMusic(); // Stop any existing music before playing new stage music
        stageEventInstance = RuntimeManager.CreateInstance(stageBGM);
        stageEventInstance.start();
        stageEventInstance.release();
    }

    // Removed commented-out methods for GetSFXClip, GetBGMClip, and GetStageClip
}