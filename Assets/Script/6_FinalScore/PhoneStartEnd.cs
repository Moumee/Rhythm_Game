using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhoneStartEnd : MonoBehaviour
{
    [SerializeField] private Image fingerImage;
    [SerializeField] private ScoreScroller scoreScroller;

    public void ShowFinger()
    {
        fingerImage.color = Color.white;
    }

    public void StartScoreScroll()
    {
        scoreScroller.StartScoreScroll();
    }
}
