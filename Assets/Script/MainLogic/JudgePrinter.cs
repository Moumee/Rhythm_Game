using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgePrinter : MonoBehaviour
{
    public Animator missText;
    public Animator goodText;
    public Animator perfectText;
    public GameObject textEffectObj;
    

    public void JudgePrint()
    {
        perfectText.SetTrigger("Perfect");
    }
}
