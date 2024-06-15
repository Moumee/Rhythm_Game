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
    private float fadeSpeed = 1f;
    bool hamsterVideoFinshed = false;
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
        AudioManager.Instance.PlayBGM(AudioManager.BGM.MainMenu);

    }

    public void Play()
    {
        chefAnimator.SetTrigger("Clicked");
        StartCoroutine(Delay());
    }

    private void Update()
    {
        if (introVideoPlayer.frame == 2)
        {
            foreach (var UI in otherUI)
            {
                UI.SetActive(false);
            }
        }
        if (introVideoPlayer.isPlaying && Input.GetKeyDown(KeyCode.Escape))
        {
            introVideoPlayer.time = introVideoPlayer.length;
        }
        if (hamsterVideoPlayer.frame == 2)
        {
            introVideoPlayer.gameObject.SetActive(false);
            AudioManager.Instance.PlayBGM(AudioManager.BGM.Restaurant);
            AudioManager.Instance.PlaySFX(AudioManager.SFX.Bell);
        }
        if (hamsterVideoFinshed)
        {
            alpha = Mathf.PingPong(Time.time * fadeSpeed + offset, 0.8f);
            continueTextObj.GetComponent<TextMeshProUGUI>().color = new Color(0, 0, 0, alpha);
            if (Input.anyKeyDown)
            {
                AudioManager.Instance.bgmSource.Stop();
                SceneManager.LoadSceneAsync("1-1");
            }
        }

        if (alpha < Mathf.Epsilon)
        {
            offset = -Time.time * fadeSpeed;
        }

    }

    private void PlayHamsterVideo(VideoPlayer vp)
    {
        hamsterVideoPlayer.Play();
        
    }

    
    

    IEnumerator Delay()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        yield return new WaitForSeconds(0.5f);
        forkAnimator.SetTrigger("Clicked");
        knifeAnimator.SetTrigger("Clicked");
        yield return new WaitForSeconds(1f);
        AudioManager.Instance.bgmSource.Stop();
        
        introVideoPlayer.Play();
    }
}
