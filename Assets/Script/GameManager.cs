using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent CatchBeat;
    public UnityEvent CatchNote;
    public static GameManager Instance;

    private List<int> testChart = new List<int> { 1, 1, 0, 0, 1, 0, 1, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 };
   
    public GameObject[] notePoints;

    public float BPM = 105;
    private float interval;
    private float margin = 0.114f;
    private float inputError;
    private float timer;
    private bool socoreChance = false;

    public int count = 0;


    public int Score = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        interval = 60 / BPM;
        timer = Time.time;
    }


    // Update is called once per frame
    void Update()
    {

        if (Time.time - timer >= interval)
        {
            if (testChart[count] == 1) 
            {
                CatchBeat.Invoke();
            }
            ++count;
            timer = Time.time;
            if (testChart[count - 1] == 0) { 
            }
            
        }

        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            inputError = Time.time - timer;

            if (inputError < interval/2)
            {
                if(inputError <= margin && testChart[count-1] == 1)
                {
                    ++Score;
                }
            }
            else
            {
                if (inputError > interval - margin && testChart[count] == 1)
                {
                    ++Score;
                }
            }
        }
    }
}
