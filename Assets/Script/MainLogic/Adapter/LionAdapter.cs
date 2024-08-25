using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LionAdapter : EventAdapter
{
    public CabbageManager cabbageManager;
    public TomatoManager tomatoManager;
    public TangerineCandyManager tangerineCandyManager;



    // Start is called before the first frame update


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

    public override void Event_CatchNote()
    {
        if (GameManager.Instance.currentStage == 0)
        {
            cabbageManager.OnNoteHit();
        }
        else if (GameManager.Instance.currentStage == 1)
        {
            tomatoManager.OnNoteHit();
        }
        else
        {
            tangerineCandyManager.OnRightNoteHit();
        }
    }
    public override void Event_MissNote()
    {
        if (GameManager.Instance.currentStage == 0)
        {
            cabbageManager.OnNoteHit();
        }
        else if (GameManager.Instance.currentStage == 1)
        {
            
        }
        else
        {
            tangerineCandyManager.OnNoteMiss();
        }
    }

    public void Event_SpawnIngre()
    {

    }
}
