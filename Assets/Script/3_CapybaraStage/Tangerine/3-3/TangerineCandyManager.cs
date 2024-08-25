using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangerineCandyManager : MonoBehaviour
{
    public EventReference syrupSFX;
    public TangerineCandy[] tangerineCandies;
    public Transform[] candyPoints;
    public TangerineCandy currentCandy;
    public int keyPressCount = 0;
    public bool isMoving = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var tangerineCandy in tangerineCandies)
        {
            if (tangerineCandy.transform.position == candyPoints[1].position)
            {
                currentCandy = tangerineCandy;
                break;
            }
        }
        

        if (keyPressCount == 4)
        {
            MoveAllCandiesLeft();
            keyPressCount = 0;
        }


    }

    void MoveAllCandiesLeft()
    {
        
        foreach (var tangerineCandy in tangerineCandies)
        {
            tangerineCandy.MoveLeft();
            
        }
    }

    public void OnRightNoteHit()
    {
        if (!isMoving)
        {
            AudioManager.Instance.PlaySFX(syrupSFX);
            currentCandy.TurnRight();
            keyPressCount++;
        }
        
    }

    public void OnLeftNoteHit()
    {
        if (!isMoving)
        {
            AudioManager.Instance.PlaySFX(syrupSFX);
            currentCandy.TurnLeft();
            keyPressCount++;
        }
        
    }
    
    public void OnNoteMiss()
    {
        if (!isMoving)
        {
            keyPressCount++;
        }
        
    }


}
