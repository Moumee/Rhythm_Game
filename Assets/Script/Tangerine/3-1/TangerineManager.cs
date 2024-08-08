using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangerineManager : MonoBehaviour
{
    public Tangerine[] tangerines;
    public Tangerine currentTangerine;
    private bool hasMoved = false;
    public Transform[] tangerinePoints;
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var tangerine in tangerines)
        {
            if (tangerine.transform.position == tangerinePoints[1].position)
            {
                currentTangerine = tangerine;
                break;
            }
        }
        if (currentTangerine != null)
        {
            if (currentTangerine.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 &&
            !currentTangerine.animator.IsInTransition(0) &&
            currentTangerine.animator.GetCurrentAnimatorStateInfo(0).IsName("tangerine_peel"))
            {
                MoveAllTangerinesLeft();
                hasMoved = true;
            }
        }
        
    }

    public void MoveAllTangerinesLeft()
    {
        foreach (var tangerine in tangerines)
        {
            tangerine.MoveTangerineLeft();
        }
    }

    public void ResetMoveFlag()
    {
        hasMoved = false;
    }

    public void OnNoteHit()
    {
        currentTangerine.sunHitCount++;
        currentTangerine.animator.SetInteger("SunHitCount", currentTangerine.sunHitCount);
    }
}