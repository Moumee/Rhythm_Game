using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using FMODUnity;
using UnityEngine.UI;

public class FinalScoreSceneManager : MonoBehaviour
{
    private ScoreStorage scoreStorage;
    [SerializeField] 
    private Animator phoneAnim;
    [SerializeField]
    private Animator fingerAnim;
    [SerializeField]
    private Animator videoAnim;
    [SerializeField]
    private Animator starAnim;
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

    //������������
    [SerializeField] private int successTotalScore = 3500; 

    private ReplyController replyController;

    [SerializeField] EventReference star1Audio;
    [SerializeField] EventReference star2Audio;
    [SerializeField] EventReference star3Audio;
    [SerializeField] EventReference star4Audio;
    [SerializeField] EventReference star5Audio;


    private void Awake()
    {
        replyController = FindFirstObjectByType<ReplyController>();
        scoreStorage = ScoreStorage.Instance;
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
        
    }
    public void PlayAnim()
    {
        AudioManager.Instance.PlaySFX(phoneAudio);
        phoneAnim.Play("phone_start");
        fingerAnim.Play("finger_slide");
        
        
        replyController.AllAppear();
    }

    public void PlayStar()
    {
        int starcount = 0;
        if (scoreStorage.FinalScore >= 2700) starcount += 1;
        if (scoreStorage.FinalScore >= 4700) starcount += 1;
        if (scoreStorage.FinalScore >= 6700) starcount += 1;
        if (scoreStorage.FinalScore >= 8700) starcount += 1;
        starAnim.SetInteger("starcount", starcount);
        if (starcount == 0)
        {
            AudioManager.Instance.PlaySFX(star1Audio);
        }
        else if (starcount == 1)
        {
            AudioManager.Instance.PlaySFX(star2Audio);
        }
        else if (starcount == 2)
        {
            AudioManager.Instance.PlaySFX(star3Audio);
        }
        else if (starcount == 3)
        {
            AudioManager.Instance.PlaySFX(star4Audio);
        }
        else if (starcount == 4)
        {
            AudioManager.Instance.PlaySFX(star5Audio);
        }
    }
    
    public void FinalScene()
    {
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
