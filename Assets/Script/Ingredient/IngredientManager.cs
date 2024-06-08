using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    private IngredientPool ingrepool;

    [SerializeField] GameObject[] standPoint;

    [SerializeField] Ingredient[] livingIngres;

    [SerializeField] Ingredient tempingre;

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
                living.SetNext();
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
                living.SetNext();
            }
        }
    }
}
