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