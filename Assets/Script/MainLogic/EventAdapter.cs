using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

abstract public class EventAdapter : MonoBehaviour
{
    //private enum stageState { ingredient = 0, mold = 1 };
    abstract public void Event_OnBeat();
    abstract public void Event_OnNote();
    abstract public void Event_CatchNote();



    /*
    public IngredientManager ingredientManager;
    public Punch punch;
    public MoldManager moldManager;
    public FillingManager fillingManager;

    private int moldCounter = 0;


    // Start is called before the first frame update


    public void Event_OnBeat()
    {
        if (GameManager.Instance.isStage1_2)
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
        if (GameManager.Instance.isStage1_2)
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
        if (GameManager.Instance.isStage1_2)
        {
            moldManager.EventCatchNote();
            fillingManager.FillingFall();
        }
        else
        {
            punch.OnEvent_Punch();
            ingredientManager.EventCatchNote();
        }
    }

    public void Event_SpawnIngre()
    {
        if (GameManager.Instance.isStage1_2)
        {
            moldManager.EventCatchNote();
            fillingManager.FillingFall();
        }
        else
        {
            punch.OnEvent_Punch();
            ingredientManager.EventCatchNote();
        }
    }
    */
    //public void 
}
