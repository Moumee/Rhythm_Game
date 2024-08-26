using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandaAdapter : EventAdapter
{
    public SliceHand sliceHand;
    public PickleContainer pickleContainer;
    public SeasoningManager seasoningManager;
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
            sliceHand.OnNoteHit();
        }
        else if (GameManager.Instance.currentStage == 1)
        {
            seasoningManager.OnNoteHit();
            containerCount++;
            if(containerCount == 25)
            {
                pickleContainer.OnOneThird();
            }
            else if(containerCount == 50)
            {
                pickleContainer.OnTwoThirds();
            }
            else if(containerCount == 75)
            {
                pickleContainer.OnThreeThirds();
            }
        }
    }

    public override void Event_SpawnIngre()
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
