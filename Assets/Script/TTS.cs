using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTS : MonoBehaviour
{
    public EventReference TTSEvent;
    
    public void PlayTTS()
    {
        AudioManager.Instance.PlaySFX(TTSEvent);
    }
}
