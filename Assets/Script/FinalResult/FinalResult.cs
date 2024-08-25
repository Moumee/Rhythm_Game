using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalResult : MonoBehaviour
{
    public EventReference videoAudio;
    public EventReference bgm;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlaySFX(videoAudio);
        AudioManager.Instance.PlayBGM(bgm);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
