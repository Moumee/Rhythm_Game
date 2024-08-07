using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangerineFall : MonoBehaviour
{
    [SerializeField] private Stick stick;
    public PointSO pointSO;
    private Vector3[] stickPoints;
    public int tangerineIndex;
    public bool tangerineStuck;
    public float fallSpeed;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        stickPoints = pointSO.stickPoints;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, stick.rotationPoint.position) > 12)
        {
            spriteRenderer.enabled = false;
        }
        else
        {
            spriteRenderer.enabled = true;
        }
    }

    public void DropTangerine()
    {
        StartCoroutine(DropTangerineCoroutine());
    }

    IEnumerator DropTangerineCoroutine()
    {
        while (!tangerineStuck)
        {
            if (transform.position.y > stickPoints[tangerineIndex].y)
            {
                transform.position -= new Vector3(0, fallSpeed, 0) * Time.deltaTime;
                yield return null;
            }
            else
            {
                transform.position = stickPoints[tangerineIndex];
                tangerineStuck = true;
                
            }
        }
    }

}
