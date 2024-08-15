using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    private IngredientPool ingrepool;

    [SerializeField] GameObject[] standPoint;

    private Ingredient[] livingIngres;
    private Ingredient[] livingIngres2;

    public Ingredient tempingre;

    private void Awake()
    {
        ingrepool = GetComponent<IngredientPool>();
    }

    public void OnEvent_SpawnIngredient()
    {
        tempingre = ingrepool.pool.Get();
        tempingre.SetPoint(standPoint);
    }

    public void OnEvent_MoveIngredient()
    {
        livingIngres = this.gameObject.GetComponentsInChildren<Ingredient>();
        foreach (Ingredient living in livingIngres)
        {
            if (living.isLive)
            {
                living.Event_BeatCall();
            }
        }
    }

    public void OnEvent_CatchNote()
    {
        livingIngres = this.gameObject.GetComponentsInChildren<Ingredient>();
        foreach (Ingredient living in livingIngres)
        {
            if (living.isLive)
            {
                //living.SetNext();
            }
        }
    }

    public void EventCatchNote()
    {
        livingIngres2 = this.gameObject.GetComponentsInChildren<Ingredient>();
        foreach (Ingredient living in livingIngres2)
        {
            if (living.isOnTime)
            {
                living.Break();
            }
        }
    }
}
