using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent CatchBeat;

    public float beat = 1f;
    public int count = 0;
    private float timer;

    void Awake()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time -timer > beat)
        {
            ++count;
            timer = Time.time;
            CatchBeat.Invoke();
        } 
    }
}
