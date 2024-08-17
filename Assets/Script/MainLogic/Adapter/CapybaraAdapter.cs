using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapybaraAdapter : EventAdapter
{
    public TangerineManager tangerineManager;
    public TangerineFallManger tangerineFallManger;
    public TangerineCandyManager tangerineCandyManager;



    // Start is called before the first frame update


    public override void Event_OnBeat()
    {
        if(GameManager.Instance.currentStage == 0)
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
            tangerineManager.OnNoteHit();
        }
        else if (GameManager.Instance.currentStage == 1)
        {
            tangerineFallManger.OnNoteHit();
        }
        else
        {
            tangerineCandyManager.OnRightNoteHit();
        }
    }

    public void Event_SpawnIngre()
    {

    }
}
