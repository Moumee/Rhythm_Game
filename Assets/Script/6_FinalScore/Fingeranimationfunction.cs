using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fingeranimationfunction : MonoBehaviour
{
    [SerializeField] ReplyController replyController;
    [SerializeField] FinalScoreSceneManager finalScoreSceneManager;
    
    public void AllSlide()
    {
        replyController.AllSlide();
    }

    public void FinalScene()
    {
        finalScoreSceneManager.FinalScene();
    }
}
