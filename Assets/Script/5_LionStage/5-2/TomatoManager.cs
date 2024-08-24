using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TomatoManager : MonoBehaviour
{
    public Tomato[] tomatoes;
    public List<Tomato> availableTomatoes = new List<Tomato>();
    public Tomato currentTomato;
    public List<Tomato> movingTomatoes = new List<Tomato>();    

    // Start is called before the first frame update
    void Start()
    {
        foreach (var tomato in tomatoes)
        {
            availableTomatoes.Add(tomato);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (movingTomatoes.Count != 0)
        {
            currentTomato = movingTomatoes[0];
        }

        //Test Code
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    TomatoAppear();
        //}
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    currentTomato.OnNoteHit();
        //}
    }

    //Need to execute this method about 2~3 beats earlier than the next 1 in note chart.
    public void TomatoAppear()
    {
        if (availableTomatoes.Count > 0)
        {
            availableTomatoes[Random.Range(0, availableTomatoes.Count)].Appear();
        }
       
    }

    public void OnNoteHit()
    {
        currentTomato.OnNoteHit();
    }
}
