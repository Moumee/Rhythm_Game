using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using FMODUnity;
using FMOD.Studio;
using Unity.VisualScripting;

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
    public EventReference knifeCut;
    public EventReference slap;
    public EventReference cuttingBoard;
    public EventReference tangerineStick;
    public EventReference hamsterGreetingTTS;
    public EventReference hamsterHappyTTS;
    public EventReference hamsterAngryTTS;
    

    public Dictionary<EventInstance, Coroutine> activeInstances = new Dictionary<EventInstance, Coroutine>();
    public EventInstance bgmEventInstance;


    public FMOD.Studio.VCA sfxVCA;
    public FMOD.Studio.VCA bgmVCA;


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
        sfxVCA = FMODUnity.RuntimeManager.GetVCA("vca:/SFX");
        bgmVCA = FMODUnity.RuntimeManager.GetVCA("vca:/BGM");
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
        EventInstance instance = RuntimeManager.CreateInstance(sound);
        Coroutine coroutine = StartCoroutine(PlaySFXCoroutine(instance));
        activeInstances.Add(instance, coroutine);
    }

    private IEnumerator PlaySFXCoroutine(EventInstance instance)
    {
        instance.start();

        PLAYBACK_STATE playbackState;
        do
        {
            instance.getPlaybackState(out playbackState);
            yield return null;
        } while (playbackState != PLAYBACK_STATE.STOPPED);

        instance.release();
        activeInstances.Remove(instance);
    }

    public void PauseAllSFX(bool pause)
    {
        foreach (var kvp in activeInstances)
        {
            kvp.Key.setPaused(pause);
        }
    }

    private void OnDisable()
    {
        // Clean up any active instances when the script is disabled or the object is destroyed
        foreach (var kvp in activeInstances)
        {
            StopCoroutine(kvp.Value);
            kvp.Key.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            kvp.Key.release();
        }
        activeInstances.Clear();
    }
    //public void PlayStageMusic(EventReference stageBGM)
    //{
    //    StopAllMusic(); // Stop any existing music before playing new stage music
    //    stageEventInstance = RuntimeManager.CreateInstance(stageBGM);
    //    stageEventInstance.start();
    //}

    // Removed commented-out methods for GetSFXClip, GetBGMClip, and GetStageClip
}