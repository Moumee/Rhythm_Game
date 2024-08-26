using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    private IngredientPool ingrePool;

    public Transform[] standPoints;

    public List<Ingredient> ingredients = new List<Ingredient>();

    public Ingredient tempIngre;

    private Ingredient startIngre;

    private void Awake()
    {
        ingrePool = GetComponent<IngredientPool>();
        
    }

    private void Start()
    {
        startIngre = ingrePool.pool.Get();
        startIngre.transform.position = standPoints[1].position;
        startIngre.positionId = 1;
    }

    public void OnEvent_SpawnIngredient()
    {
        tempIngre = ingrePool.pool.Get();
        tempIngre.SetPoint(standPoints);
    }

    //Must be executed 2~3 beats prior.
    public void MoveIngredients()
    {
        
        
    }

    

    
}
