using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldManager : MonoBehaviour
{
    MoldPool moldPool;
    public Transform[] standPoints;
    public Mold currentMold;

    FillingManager fillingManager;

    public int callNumber = 0;

    public List<Mold> activeMolds = new List<Mold>();

    [SerializeField] Mold tempMold;

    Mold startMold;

    public int spawnInterval = 0;

    private void Awake()
    {
        fillingManager = FindObjectOfType<FillingManager>();
        moldPool = GetComponent<MoldPool>();
    }

    private void Start()
    {
        startMold = moldPool.pool.Get();
        startMold.transform.position = standPoints[1].position;
        startMold.positionId = 1;
        activeMolds.Add(startMold);
    }

    private void Update()
    {
        foreach (var mold in activeMolds)
        {
            if (mold.transform.position == standPoints[1].position)
            {
                currentMold = mold;
                break;
            }
        }

        //Test Code
       
    }


    public void SpawnMold()
    {
        tempMold = moldPool.pool.Get();
        tempMold.SetPoint(standPoints);
        activeMolds.Add(tempMold);
    }

    public void MoveMolds()
    {
        SpawnMold();
        foreach (var mold in activeMolds)
        {
            mold.MoveMold();
        }
        
    }

    public void OnNoteHit()
    {
        callNumber++;
        fillingManager.FillingFall(callNumber);
        if (callNumber == 3)
        {

            StartCoroutine(DelayMovement(0.1f));
            
        }
        
    }

    public void OnNoteMiss()
    {
        callNumber++;
        if (callNumber == 3)
        {
            StartCoroutine(DelayMovement(0.1f));
            callNumber = 0;
        }
    }

    IEnumerator DelayMovement(float delayDuration)
    {
        yield return new WaitForSeconds(delayDuration);
        MoveMolds();
        callNumber = 0;
    }

    //IEnumerator MoldDelay()
    //{
    //    yield return null;
    //    //yield return new WaitForSeconds(1 * (60 / GameManager.Instance.BPM-0.5f));
    //    tempMold = moldPool.pool.Get();
    //    tempMold.SetPoint(standPoint);
    //    spawnInterval++;
    //    livingMolds = this.gameObject.GetComponentsInChildren<Mold>();
    //    foreach (Mold living in livingMolds)
    //    {
    //        if (living.isLive)
    //        {
    //            living.SetNext();
    //        }
    //    }
    //}
}
    
