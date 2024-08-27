using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamsterAdapter : EventAdapter
{
    [SerializeField] IngredientManager ingredientManager;
    public MoldManager moldManager;






    public override void Event_OnBeat()
    {
        if (GameManager.Instance.currentStage == 0)
        {
            
        }
        else if (GameManager.Instance.currentStage == 1)
        {

        }
    }

    public override void Event_OnNote()
    {
        if (GameManager.Instance.currentStage == 0)
        {

        }
        else if (GameManager.Instance.currentStage == 1)
        {

        }
    }

    public override void Event_CatchNote(bool isPerfect = true, int direction = 0)
    {
        if (GameManager.Instance.currentStage == 0)
        {
            ingredientManager.OnNoteHit();
        }
        else if (GameManager.Instance.currentStage == 1)
        {
            moldManager.OnNoteHit();
        }
    }

    public override void Event_SpawnIngre()
    {
        if (GameManager.Instance.currentStage == 0)
        {
            ingredientManager.MoveIngredients();  
        }
        else if (GameManager.Instance.currentStage == 1)
        {

        }
    }

    public override void Event_MissNote()
    {
        if (GameManager.Instance.currentStage == 0)
        {
            ingredientManager.OnNoteMiss();
        }
        else if (GameManager.Instance.currentStage == 1)
        {
            moldManager.OnNoteMiss();
        }
    }
}
