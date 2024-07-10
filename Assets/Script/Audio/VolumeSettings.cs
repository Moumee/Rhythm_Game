using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    
    
    
    private enum VoulmeType
    {
        MASTER,
        BGM,
        SFX
    }

    [Header("Type")]
    [SerializeField] private VoulmeType volumeType;


    private Slider volumeSlider;

    private void Awake()
    {
        volumeSlider = GetComponentInChildren<Slider>();
    }

    private void Update()
    {
        switch (volumeType)
        {
            case VoulmeType.MASTER:
                volumeSlider.value = AudioManager.Instance.masterVolume;
                break;
            case VoulmeType.BGM:
                volumeSlider.value = AudioManager.Instance.bgmVolume;
                break;
            case VoulmeType.SFX:
                volumeSlider.value = AudioManager.Instance.sfxVolume;
                break;
            default:
                Debug.LogWarning("VoulmeType error");
                break;
        }
    }

    public void OnSliderValueChanged()
    {
        switch (volumeType)
        {
            case VoulmeType.MASTER:
                AudioManager.Instance.masterVolume = volumeSlider.value;
                break;
            case VoulmeType.BGM:
                AudioManager.Instance.bgmVolume = volumeSlider.value;
                break;
            case VoulmeType.SFX:
                AudioManager.Instance.sfxVolume = volumeSlider.value;
                break;
            default:
                Debug.LogWarning("VoulmeType error");
                break;
        }
    }
}
