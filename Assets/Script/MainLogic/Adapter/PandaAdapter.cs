using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaAdapter : EventAdapter
{
    public SliceHand sliceHand;
    public PickleContainer pickleContainer;
    public SeasoningManager seasoningManager;
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
            sliceHand.OnNoteHit();
        }
        else if (GameManager.Instance.currentStage == 1)
        {
            seasoningManager.OnNoteHit();
        }
    }

    public void Event_SpawnIngre()
    {

    }

    public override void Event_MissNote()
    {
        if (GameManager.Instance.currentStage == 0)
        {
            sliceHand.OnNoteMiss();
        }
        else if (GameManager.Instance.currentStage == 1)
        {
            seasoningManager.OnNoteMiss();
        }
    }

}
