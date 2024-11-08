using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class StartMenu : MonoBehaviour
{
    [SerializeField] VideoPlayer introVideoPlayer;
    [SerializeField] VideoPlayer loadingVideoPlayer;
    [SerializeField] GameObject[] otherUI;
    [SerializeField] Animator chefAnimator;
    [SerializeField] Animator forkAnimator;
    [SerializeField] Animator knifeAnimator;
    [SerializeField] GameObject continueTextObj;
    [SerializeField] GameObject pauseController;
    [SerializeField] GameObject fade;

    private void Awake()
    {
        introVideoPlayer.loopPointReached += PlayLoadingVideo;
        loadingVideoPlayer.loopPointReached += LoadHamsterIntro;
    }

    private void Start()
    {
        AudioManager.Instance.PlayBGM(AudioManager.Instance.mainMenu);
    }

    public void Play()
    {
        chefAnimator.SetTrigger("Clicked");
        StartCoroutine(Delay());
    }

    private void Update()
    {
        
        if (introVideoPlayer.frame == 1)
        {
            foreach (var UI in otherUI)
            {
                UI.SetActive(false);
            }
        }

        if (loadingVideoPlayer.frame == 1)
        {
            introVideoPlayer.gameObject.SetActive(false);
        }

    }

    private void PlayLoadingVideo(VideoPlayer vp)
    {
        StartCoroutine(FadeToLoading());
    }

    private void LoadHamsterIntro(VideoPlayer vp)
    {
        SceneTransitionManager.LoadSceneWithTransition("HamsterIntro");
    }

    IEnumerator FadeToLoading()
    {
        fade.SetActive(true);
        fade.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.5f);
        pauseController.SetActive(true);
        loadingVideoPlayer.Play();
        yield return new WaitForSeconds(0.2f);
        fade.GetComponent<Animator>().SetTrigger("FadeIn");
        yield return new WaitForSeconds(0.5f);
        fade.SetActive(false);
    }

    IEnumerator Delay()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(0.5f);
        forkAnimator.SetTrigger("Clicked");
        knifeAnimator.SetTrigger("Clicked");
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.StopAllMusic(); // Updated this line

        introVideoPlayer.Play();
        AudioManager.Instance.PlayBGM(AudioManager.Instance.introVideoAudio);
    }

}