using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting.Antlr3.Runtime.Misc;

public class ScoreScroller : MonoBehaviour
{
    public RectTransform unitsRT;
    public RectTransform tensRT;
    public RectTransform hundredsRT;
    public RectTransform thousandsRT;
    
    
    public GameObject blueNumberPrefab;
    public GameObject redNumberPrefab;
    private GameObject numberPrefab;
    private int height = 100;
    public int score = 9999; 
    public int cycles = 10;
    
    private void Awake()
    {
        score = ScoreStorage.Instance.FinalScore;
        if (score >= 3500)
        {
            numberPrefab = blueNumberPrefab;
        }
        else
        {
            numberPrefab = redNumberPrefab;
        }
        InitDigit(score / 1000, thousandsRT);
        InitDigit((score % 1000) / 100, hundredsRT);
        InitDigit((score % 100) / 10, tensRT);
        InitDigit(score % 10, unitsRT);
            

    }

    private void Update()
    {

    }

    public void StartScoreScroll()
    {
        MoveDigit(unitsRT, 0);
        MoveDigit(tensRT, 2);
        MoveDigit(hundredsRT, 4);
        MoveDigit(thousandsRT, 6);
    }
    
    void InitDigit(int targetNumber, RectTransform RT)
    {
        int steps = cycles * 10;
        for (int i = 0; i < steps; i++)
        {
            GameObject numberObj = Instantiate(numberPrefab, RT.transform);
            RectTransform numberRT = numberObj.GetComponent<RectTransform>();
            numberRT.anchoredPosition = new Vector2(0, (steps - i) * height);
            TextMeshProUGUI numberTMP = numberObj.GetComponent<TextMeshProUGUI>();
            numberTMP.text = ((i + targetNumber) % 10).ToString();
        }
        
    }

    IEnumerator MoveUnitsCoroutine()
    {
        Tween tween = unitsRT.DOAnchorPosY(-cycles * height * 10, 3.4f, true)
            .SetEase(Ease.OutCubic);
        yield return tween.WaitForCompletion();
    }

    
    void MoveDigit(RectTransform inRT, int cycleSubtract)
    {
        inRT.DOAnchorPosY(-cycles * height * 10, 3.4f, true)
            .SetEase(Ease.OutCubic)
            .OnUpdate(() => 
            {
                if (inRT.anchoredPosition.y <= -cycles * height * 10 + 100 * cycleSubtract * 10)
                {
                    DOTween.Kill(inRT);
                }
            });
    }
}