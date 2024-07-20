using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Fish : MonoBehaviour
{
    public PointSO pointData;
    private float speed = 100f;
    public int positionId = 0;
    private FishManager fishManager;

    private void Awake()
    {
        fishManager = FindObjectOfType<FishManager>();
    }


    // Update is called once per frame
    void Update()
    {
        

        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, pointData.fishWaypoints[positionId], step);
    }

    public void MoveFish()
    {   
        if (positionId < pointData.fishWaypoints.Length - 1)
        {
            positionId++;
        }
        else if (positionId == pointData.fishWaypoints.Length - 1)
        {
            gameObject.transform.position = pointData.fishWaypoints[0];
            positionId = 0;
        }
    }


}
