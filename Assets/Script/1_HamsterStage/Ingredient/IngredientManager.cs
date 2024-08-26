using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    public Punch punch;

    private IngredientPool ingrePool;

    public Transform[] standPoints;

    public List<Ingredient> activeIngredients = new List<Ingredient>();

    public Ingredient tempIngre;

    private Ingredient startIngre;

    public Ingredient targetIngre;

    private void Awake()
    {
        ingrePool = GetComponent<IngredientPool>();
        
    }

    private void Start()
    {
        startIngre = ingrePool.pool.Get();
        startIngre.transform.position = standPoints[1].position;
        startIngre.positionId = 1;
        startIngre.GetComponent<Animator>().Play("shaking");
        activeIngredients.Add(startIngre);
    }

    private void Update()
    {
        //Test Code
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MoveIngredients();
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            OnNoteHit();
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            OnNoteMiss();
        }

        foreach (var ingredient in activeIngredients)
        {
            if (ingredient.transform.position.x < 3 && ingredient.transform.position.x > -3)
            {
                targetIngre = ingredient;
            }
        }
    }

    public void OnNoteHit()
    {
        punch.OnNoteHit();
        if (targetIngre != null)
        {
            targetIngre.Break();
        }
        
    }

    public void OnNoteMiss()
    {
        punch.OnNoteMiss();
    }

    public void SpawnIngredient()
    {
        tempIngre = ingrePool.pool.Get();
        tempIngre.SetPoint(standPoints);
        activeIngredients.Add(tempIngre);
    }

    //Must be executed 2~3 beats prior.
    public void MoveIngredients()
    {
        SpawnIngredient();
        foreach (var ingredient in activeIngredients)
        {
            ingredient.MoveNext();
        }
        
    }

    

    
}
