using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    VideoPlayer introVideoPlayer;
    private void Awake()
    {
        introVideoPlayer = GetComponent<VideoPlayer>();
    }
    public void PlayIntroVideo()
    {
        introVideoPlayer.Play();
        
    }
}
