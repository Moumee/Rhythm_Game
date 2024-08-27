using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEffectMove : MonoBehaviour
{
    public Transform[] noteCenter;

    public void EffectMove(int stage)
    {
        if(GameManager.Instance.stageNumber == GameManager.numberofStage._3Capybara
            && GameManager.Instance.currentStage == 0)
        {
            transform.position = noteCenter[stage].position + Vector3.down * 1;
        }
        else
        {
            transform.position = noteCenter[stage].position + Vector3.down * 4;
        }
        
    }
}
