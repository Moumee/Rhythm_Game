using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class FinalScoreSceneManager : MonoBehaviour
{
    [SerializeField] 
    private Animator phoneAnim;
    [SerializeField]
    private Animator fingerAnim;
    [SerializeField]
    private Animator videoAnim;
    bool videoEnd = false;
    private void Awake()
    {
       
    }

    

    private void Update()
    {
        if (videoAnim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1f && !videoEnd)
        {
            PlayeAnim();
            videoEnd = true;
        }
        
    }
    public void PlayeAnim()
    {
        phoneAnim.Play("phone_start");
        fingerAnim.Play("finger_slide");
    }
    

    
}
