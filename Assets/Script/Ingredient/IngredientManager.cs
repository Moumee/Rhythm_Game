using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    private IngredientPool ingrepool;

    [SerializeField] GameObject[] standPoint;

    [SerializeField] GameObject[] livingIngres;

    private void Awake()
    {
        ingrepool = GetComponent<IngredientPool>();
    }

    public void SpawnIngredient()
    {
        Ingredient tempingre = ingrepool.pool.Get();
        tempingre.SetPoint(standPoint);
    }

    public void MoveIngredient()
    {
        livingIngres = this.GetComponentsInChildren<GameObject>();
        foreach (GameObject living in livingIngres)
        {
            if (living.active)
            {
                living.GetComponent<Ingredient>().SetNext();
            }
        }
    }
}
