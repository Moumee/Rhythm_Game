using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterAngryTTS : MonoBehaviour
{

    public void PlayHamsterAngryTTS()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.hamsterAngryTTS);
    }
}
