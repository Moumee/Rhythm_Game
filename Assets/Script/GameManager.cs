using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent CatchBeat;
    public static GameManager Instance;
   
    public GameObject[] notePoints;

    public float beat = 1f;
    public int count = 0;

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
    }

    private void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
