using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatEventAdapter : MonoBehaviour
{
    private enum stageState { ingredient = 0, mold = 1 };

    public IngredientManager ingredientManager;
    public TestScript testScript;
    public MoldManager moldManager;
    public FishManager fishManager;

    private int moldCounter = 0;


    // Start is called before the first frame update


    public void Event_OnBeat()
    {
        Debug.Log("ffffff");
        if (CatGameManager.Instance.isStage1_2)
        {
            moldManager.OnEvent_MoveMold();
        }
        else
        { 
            ingredientManager.OnEvent_MoveIngredient();
        }
    }

    public void Event_OnNote()
    {
        if (CatGameManager.Instance.isStage1_2)
        {
            Debug.Log("f");
            moldManager.OnEvent_SpawnMold();
        }
        else
        {
            ingredientManager.OnEvent_SpawnIngredient();
        }
    }

    public void Event_CatchNote()
    {
        if (CatGameManager.Instance.isStage1_2)
        {
            moldManager.EventCatchNote();
            //fishManager
        }
        else
        {
            testScript.Slap();
            ingredientManager.EventCatchNote();
        }
    }
}
