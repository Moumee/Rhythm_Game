using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class StartMenu : MonoBehaviour
{
    [SerializeField] VideoPlayer introVideoPlayer;
    [SerializeField] GameObject canvas;
    [SerializeField] Animator chefAnimator;
    [SerializeField] Animator forkAnimator;
    [SerializeField] Animator knifeAnimator;

    private void Awake()
    {
        introVideoPlayer.loopPointReached += LoadNextScene;
    }
    public void Play()
    {
        chefAnimator.SetTrigger("Clicked");
        StartCoroutine(Delay());
    }

    private void Update()
    {
        if (introVideoPlayer.isPlaying)
        {
            canvas.SetActive(false);
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
        introVideoPlayer.time = 0;
        introVideoPlayer.Play();
    }
}
