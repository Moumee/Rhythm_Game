using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAdapter : MonoBehaviour
{
    private enum stageState { ingredient = 0, mold = 1 };

    //public IngredientManager ingredientManager;
    public FishManager fishManager;
    public CatGameManager catGameManager;
    public TestScript testscript;

    private int moldCounter = 0;


    // Start is called before the first frame update


    public void Event_OnBeat()
    {
        if (GameManager.Instance.isStage1_2)
        {
            catGameManager.Fish();
            //moldManager.OnEvent_MoveMold();
        }
        else
        { 
            //ingredientManager.OnEvent_MoveIngredient();
        }
    }

    public void Event_OnNote()
    {
        if (GameManager.Instance.isStage1_2)
        {
            Debug.Log("f");
            //moldManager.OnEvent_SpawnMold();
        }
        else
        {
            //ingredientManager.OnEvent_SpawnIngredient();
        }
    }

    public void Event_CatchNote()
    {
        if (GameManager.Instance.isStage1_2)
        {
            //moldManager.EventCatchNote();
            testscript.Chop();
        }
        else
        {
            testscript.Slap();
            //ingredientManager.EventCatchNote();
        }
    }
}
