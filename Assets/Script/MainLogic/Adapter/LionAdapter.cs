using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionAdapter : EventAdapter
{
    public CabbageManager cabbageManager;
    public TomatoManager tomatoManager;
    public Mushroom mushroom;
    public CheeseGrater cheeseGrater;
    public BackgroundCheese backgroundCheese;
    public BackgroundScratch backgroundScratch;

    private int containerCount = 0;
    [SerializeField]private int mushroomGoodCount = 0;
    [SerializeField]private int mushroomMissCount = 0;
    private int scratchCount = 0;

    // Start is called before the first frame update

    private void Awake()
    {
        
    }

    public override void Event_OnBeat()
    {
        if (GameManager.Instance.currentStage == 0)
        {

        }
        else if (GameManager.Instance.currentStage == 1)
        {

        }
        else if (GameManager.Instance.currentStage == 2)
        {
            
        }
        else
        {

        }
    }

    public override void Event_OnNote()
    {

    }
    
    public override void Event_CatchNote(bool isPerfect = true, int direction = 0)
    {
        if (GameManager.Instance.currentStage == 0)
        {
            cabbageManager.OnNoteHit();
        }
        else if (GameManager.Instance.currentStage == 1)
        {
            tomatoManager.OnNoteHit();
            scratchCount++;
            if (scratchCount == 25)
            {
                backgroundScratch.OnOneThird();
            }
            else if (scratchCount == 50)
            {
                backgroundScratch.OnTwoThirds();
            }
            
        }
        else if (GameManager.Instance.currentStage == 2)
        {
            mushroomGoodCount++;
            
            if (isPerfect)
            {
                if(direction == 1)
                {
                    mushroom.OnPerfectRightNoteHit();
                }
                else
                {
                    mushroom.OnPerfectLeftNoteHit();
                }
            }
            else
            {
                if (direction == 1)
                {
                    mushroom.OnRightNoteHit();
                }
                else
                {
                    mushroom.OnLeftNoteHit();
                }
            }
            
            if (mushroomGoodCount == 20) 
            {
                mushroom.CookMushroom();
            }

        }
        else
        {
            if(!isPerfect) { cheeseGrater.OnGoodNoteHit(); }
            else { cheeseGrater.OnPerfectNoteHit(); }
            
            containerCount++;
            if (containerCount == 16)
            {
                backgroundCheese.FirstPile();
            }
            else if (containerCount == 32)
            {
                backgroundCheese.SecondPile();
            }
            else if (containerCount == 48)
            {
                backgroundCheese.ThirdPile();
            }
            else if(containerCount == 64)
            {
                backgroundCheese.FourthPile();
            }
        }
    }
    public override void Event_MissNote(bool passThrough = false)
    {
        if (GameManager.Instance.currentStage == 0)
        {
            cabbageManager.OnNoteMiss(passThrough);
            
        }
        else if (GameManager.Instance.currentStage == 1 && !passThrough)
        {
            tomatoManager.OnNoteMiss();
        }
        else if (GameManager.Instance.currentStage == 2 && passThrough)
        {
            mushroomMissCount++;
            if (mushroomMissCount >= 20)
            {
                mushroom.BurnMushroom();
            }
        }
        else if (!passThrough) 
        {
            cheeseGrater.OnNoteMiss();
        }
    }

    public override void Event_SpawnIngre()
    {
        if (GameManager.Instance.currentStage == 0)
        {

        }
        else if (GameManager.Instance.currentStage == 1)
        {
            tomatoManager.TomatoAppear();
        }
        else if (GameManager.Instance.currentStage == 2)
        {
            
        }
        else
        {

        }
        
    }
}
