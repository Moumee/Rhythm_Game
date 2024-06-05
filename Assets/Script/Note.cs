using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] GameObject[] standPoint;
    Vector3 targetPostion;
    [SerializeField] int positionId = 0;
   
    // Start is called before the first frame update
    void Awake()
    {
        transform.position = standPoint[positionId].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, standPoint[positionId].transform.position, Time.deltaTime * 10f);
    }

    public void SetNext()
    {
        if (positionId < standPoint.Length-1) 
        { 
            ++positionId; 
        }
        
    }

    public void SetStandPoint(GameObject[] pointList)
    {
        standPoint = pointList;
    }
}
