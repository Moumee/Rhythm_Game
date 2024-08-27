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
        if (ScoreStorage.Instance.FinalScore >= successTotalScore)
        {
            SceneManager.LoadSceneAsync("SuccessResult");
        }
        else
        {
            SceneManager.LoadSceneAsync("FailResult");
        }
    }
}
