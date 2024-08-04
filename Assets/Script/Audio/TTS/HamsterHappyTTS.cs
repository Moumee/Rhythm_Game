using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterHappyTTS : MonoBehaviour
{
    public void PlayHamsterHappyTTS()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.hamsterHappyTTS);
    }
}
