using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangerineFall : MonoBehaviour
{
    public PointSO pointSO;
    private Vector3[] stickPoints;
    public int tangerineIndex;
    public bool tangerineStuck;
    public float fallSpeed;

    private void Awake()
    {
        stickPoints = pointSO.stickPoints;
    }

    private void Update()
    {
        
    }

    public void DropTangerine()
    {
        if (!tangerineStuck)
        {
            if (transform.position.y > stickPoints[tangerineIndex].y)
            {
                transform.position -= new Vector3(0, fallSpeed, 0) * Time.deltaTime;
            }
            else
            {
                transform.position = stickPoints[tangerineIndex];
                tangerineStuck = true;
            }
        }
    }
}
