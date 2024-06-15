using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillingManager : MonoBehaviour
{
    [SerializeField] GameObject fillingPrefab;
    public int callNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillingFall()
    {
        switch (callNumber)
        {
            case 0:
                Instantiate(fillingPrefab);
                callNumber++;
                break;
            case 1:
                Instantiate(fillingPrefab);
                callNumber++;
                break;
            case 2:
                Instantiate(fillingPrefab);
                callNumber = 0;
                break;
            default:
                break;
        }
    }
}
