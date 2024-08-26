using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatAdapter : EventAdapter { 
    public SlapManager slapManager;
    public FishManager fishManager;
    // Start is called before the first frame update

    private int containerCount = 0;

    public override void Event_OnBeat()
    {
        if (GameManager.Instance.currentStage == 0)
        {
            
        }
        else if (GameManager.Instance.currentStage == 1)
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
            slapManager.OnLeftNoteHit();
        }
        else if (GameManager.Instance.currentStage == 1)
        {
            fishManager.OnNoteHit();
        }
    }

    

    public override void Event_MissNote()
    {
        if (GameManager.Instance.currentStage == 0)
        {
            slapManager.OnNoteMiss();
        }
        else if (GameManager.Instance.currentStage == 1)
        {
            fishManager.OnNoteMiss();
        }
    }

    public override void Event_SpawnIngre()
    {

    }
}
