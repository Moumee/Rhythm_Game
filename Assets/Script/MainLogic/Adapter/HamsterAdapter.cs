using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterAdapter : EventAdapter
{
    [SerializeField] IngredientManager ingredientManager;
    public Punch punch;
    public MoldManager moldManager;
    public FillingManager fillingManager;






    public override void Event_OnBeat()
    {
        //if (GameManager.Instance.currentStage > 0)
        //{
        //    moldManager.OnEvent_MoveMold();
        //}
        //else
        //{
        //    //ingredientManager.OnEvent_MoveIngredient();
        //}
    }

    public override void Event_OnNote()
    {
        //if (GameManager.Instance.currentStage > 0)
        //{
        //    Debug.Log("f");
        //    moldManager.OnEvent_SpawnMold();
        //}
        //else
        //{
        //    ingredientManager.SpawnIngredient();
        //}
    }

    public override void Event_CatchNote(bool isPerfect = true, int direction = 0)
    {
        if (GameManager.Instance.currentStage > 0)
        {
            //moldManager.EventCatchNote();
            //fillingManager.FillingFall();
        }
        else
        {
            //punch.OnEvent_Punch();
            //ingredientManager.EventCatchNote();
        }
    }

    public override void Event_SpawnIngre()
    {
        if (GameManager.Instance.currentStage == 1)
        {
            //moldManager.EventCatchNote();
            //fillingManager.FillingFall();
        }
        else
        {
            //punch.OnEvent_Punch();
            //ingredientManager.EventCatchNote();
        }
    }

    public override void Event_MissNote()
    {

    }
}
