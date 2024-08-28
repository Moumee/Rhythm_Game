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
    [SerializeField] VideoPlayer hamsterVideoPlayer;
    [SerializeField] GameObject[] otherUI;
    [SerializeField] Animator chefAnimator;
    [SerializeField] Animator forkAnimator;
    [SerializeField] Animator knifeAnimator;
    [SerializeField] GameObject continueTextObj;
    [SerializeField] GameObject pauseController;
    [SerializeField] GameObject fade;
    [SerializeField] SceneController sceneController;
    [SerializeField] SceneFade sceneFade;
    AsyncOperation asyncOperation;
    private float fadeSpeed = 1f;
    bool hamsterVideoFinshed = false;
    bool hamsterVideoStart = false;
    float offset = 0;
    float alpha;

    private void Awake()
    {
        introVideoPlayer.loopPointReached += PlayHamsterVideo;
        hamsterVideoPlayer.loopPointReached += HamsterVideoFinished;
    }

    private void HamsterVideoFinished(VideoPlayer source)
    {
        continueTextObj.SetActive(true);
        hamsterVideoFinshed = true;
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
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartCoroutine(Fade());
        }
        if (introVideoPlayer.frame == 1)
        {
            foreach (var UI in otherUI)
            {
                UI.SetActive(false);
            }
        }

        if (hamsterVideoPlayer.frame == 1 && !hamsterVideoStart)
        {
            introVideoPlayer.gameObject.SetActive(false);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.bell);
            AudioManager.Instance.PlayBGM(AudioManager.Instance.restaurant);
            StartCoroutine(PlayHamsterGreetingTTS());
            hamsterVideoStart = true;
        }
        if (hamsterVideoFinshed)
        {
            alpha = Mathf.PingPong(Time.time * fadeSpeed + offset, 0.8f);
            continueTextObj.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, alpha);
            if (Input.anyKey)
            {
                if (!Input.GetKey(KeyCode.Escape) && !Input.GetMouseButton(0) && !Input.GetMouseButton(1))
                {
                    AudioManager.Instance.StopAllMusic(); // Updated this line
                    SceneTransitionManager.LoadSceneWithTransition("1-1");
                }
            }
        }

        if (alpha < Mathf.Epsilon)
        {
            offset = -Time.time * fadeSpeed;
        }
    }

    private void PlayHamsterVideo(VideoPlayer vp)
    {
        StartCoroutine(Fade());
    }

    

    IEnumerator FadeOutToNextScene()
    {
        fade.SetActive(true);
        fade.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.5f);
        AudioManager.Instance.StopAllMusic(); // Added this line
        
    }

    IEnumerator Fade()
    {
        fade.SetActive(true);
        fade.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(0.5f);
        pauseController.SetActive(true);
        hamsterVideoPlayer.Play();
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

    IEnumerator PlayHamsterGreetingTTS()
    {
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.hamsterGreetingTTS);
    }
}