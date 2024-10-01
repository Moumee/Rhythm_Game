using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    private enum VolumeType
    {
        MASTER,
        BGM,
        SFX
    }

    [Header("Type")]
    [SerializeField] private VolumeType volumeType;

     

    private Slider volumeSlider;
    

    private void Awake()
    {
        volumeSlider = GetComponent<Slider>();
        switch (volumeType)
        {
            case VolumeType.BGM:
                FMOD.RESULT bgmResult = AudioManager.Instance.bgmVCA.getVolume(out float bgmVolume);
                volumeSlider.value = bgmVolume;
                break;
            case VolumeType.SFX:
                FMOD.RESULT sfxResult = AudioManager.Instance.sfxVCA.getVolume(out float sfxVolume);
                volumeSlider.value = sfxVolume;
                break;
            default:
                break;
        }


    }

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }


    

    

    public void SetVolume(float volume)
    {
        switch (volumeType)
        {
            
            case VolumeType.BGM:
                AudioManager.Instance.bgmVCA.setVolume(volume);
                break;
            case VolumeType.SFX:
                AudioManager.Instance.sfxVCA.setVolume(volume);
                break;
            default:
                Debug.LogWarning("VolumeType error");
                break;
        }
    }
}