using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadFinalResult : MonoBehaviour
{
    [Header("������� ���� ����")]
    public int successTotalScore;
    public void LoadResultScene()
    {
        SceneTransitionManager.LoadSceneWithTransition("ScoreScroll");
        //if (ScoreStorage.Instance.FinalScore >= successTotalScore)
        //{
        //    SceneTransitionManager.LoadSceneWithTransition("SuccessResult");
        //}
        //else
        //{
        //    SceneTransitionManager.LoadSceneWithTransition("FailResult");
        //}
    }
}
