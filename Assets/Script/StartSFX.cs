using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSFX : MonoBehaviour
{
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();  
    }

    public void PlaySFX()
    {
        audioSource.Play();
    }
}
