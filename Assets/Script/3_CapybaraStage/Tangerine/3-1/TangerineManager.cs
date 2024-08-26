using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangerineManager : MonoBehaviour
{
    public EventReference tangerineCook;
    public EventReference tangerinePeel;
    public Tangerine[] tangerines;
    public Tangerine currentTangerine;
    private bool hasMoved = false;
    private bool peeled = false;    
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
            if (tangerine.transform.position.x < tangerinePoints[1].position.x + 5)
            {
                currentTangerine = tangerine;
                break;
            }
        }
        if (currentTangerine != null)
        {
            if (currentTangerine.sunHitCount == 2 && !peeled)
            {
                AudioManager.Instance.PlaySFX(tangerinePeel);
                peeled = true;
            }
            if (currentTangerine.sunHitCount == 3)
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

    public void ResetFlag()
    {
        hasMoved = false;
        peeled = false;
    }

    public void OnNoteHit()
    {
        AudioManager.Instance.PlaySFX(tangerineCook);
        currentTangerine.sunHitCount++;
        currentTangerine.animator.SetInteger("SunHitCount", currentTangerine.sunHitCount);
    }
}
