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
        StartCoroutine(MoveUnitsCoroutine());
        StartCoroutine(MoveDigitCoroutine(tensRT, 2));
        StartCoroutine(MoveDigitCoroutine(hundredsRT, 4));
        StartCoroutine(MoveDigitCoroutine(thousandsRT, 6));
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

    IEnumerator MoveDigitCoroutine( RectTransform inRT, int cycleSubtract)
    {
        Tween tween = inRT.DOAnchorPosY(-cycles * height * 10, 3.4f, true)
            .SetEase(Ease.OutCubic);
        while (tween.IsActive() && !tween.IsComplete())
        {
            if (inRT.anchoredPosition.y <= -cycles * height * 10 + 100 * cycleSubtract * 10)
            {
                tween.Kill();
                yield break;
            }
            yield return null;
        }
    }
}