using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class StartMenu : MonoBehaviour
{
    [SerializeField] VideoPlayer introVideoPlayer;
    [SerializeField] Animator chefAnimator;
    [SerializeField] Animator forkAnimator;
    [SerializeField] Animator knifeAnimator;

    private void Awake()
    {
        introVideoPlayer.loopPointReached += LoadNextScene;
    }

    private void Start()
    {
        AudioManager.Instance.PlayBGM("Main");

    }

    public void Play()
    {
        chefAnimator.SetTrigger("Clicked");
        StartCoroutine(Delay());
    }

    private void Update()
    {
        if (introVideoPlayer.isPlaying && Input.GetKeyDown(KeyCode.Escape))
        {
            introVideoPlayer.time = introVideoPlayer.length;
        }
    }

    void LoadNextScene(VideoPlayer vp)
    {
        SceneManager.LoadSceneAsync(1);
    }
    

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        forkAnimator.SetTrigger("Clicked");
        knifeAnimator.SetTrigger("Clicked");
        yield return new WaitForSeconds(0.5f);
        AudioManager.Instance.bgmSource.Stop();
        introVideoPlayer.time = 0;
        introVideoPlayer.Play();
    }
}
