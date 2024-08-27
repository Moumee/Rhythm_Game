using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreStorage : MonoBehaviour
{
    public int FinalScore = 0;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    
}
