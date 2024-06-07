using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ingredient : MonoBehaviour
{

    [SerializeField] int positionId = 0;

    GameObject[] standPoints;

    public float speed;
    
    // Start is called before the first frame update
    void Awake()
    {
        
    }

    private void Start()
    {
        transform.position = standPoints[positionId].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, standPoints[positionId].transform.position, step);
    }

    public void SetNext()
    {
        if (positionId == standPoints.Length - 1) return;

        if (positionId < standPoints.Length - 1) 
        { 
            ++positionId; 
        }
        
    }

}
