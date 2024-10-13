using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using FMODUnity;
using UnityEngine.UI;

public class FinalScoreSceneManager : MonoBehaviour
{
    [SerializeField] 
    private Animator phoneAnim;
    [SerializeField]
    private Animator fingerAnim;
    [SerializeField]
    private Animator videoAnim;
    bool videoEnd = false;
    bool scrollStart = false;

    [SerializeField]
    private EventReference videoAudio;
    [SerializeField]
    private EventReference phoneAudio;
    [SerializeField]
    private EventReference endingBGM;

    [SerializeField]
    private ScoreScroller scoreScroller;

    //최종점수판정
    [SerializeField] private int successTotalScore = 3500; 

    private void Awake()
    {


        AudioManager.Instance.PlaySFX(videoAudio);
        AudioManager.Instance.PlayBGM(endingBGM);
        
    }

    

    private void Update()
    {
        if (videoAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f && !videoEnd)
        {
            PlayAnim();
            videoEnd = true;
        }

        if (!phoneAnim.GetComponent<Image>().enabled && !scrollStart)
        {
            scoreScroller.StartScoreScroll();
            scrollStart = true;
        }
        
    }
    public void PlayAnim()
    {
        AudioManager.Instance.PlaySFX(phoneAudio);
        phoneAnim.Play("phone_start");
        fingerAnim.Play("finger_slide");
    }
    
    IEnumerator nextSceneDelay()
    {
        yield return new WaitForSeconds(5f);
        if (ScoreStorage.Instance.FinalScore >= successTotalScore)
        {
            SceneTransitionManager.LoadSceneWithTransition("SuccessResult");
        }
        else
        {
            SceneTransitionManager.LoadSceneWithTransition("FailResult");
        }
    }

}
