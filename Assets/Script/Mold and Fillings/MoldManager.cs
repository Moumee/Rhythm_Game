using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoldManager : MonoBehaviour
{
    MoldPool moldPool;
    [SerializeField] GameObject[] standPoint;

    private Mold[] livingMolds;
    private Mold[] livingMolds2;

    [SerializeField] Mold tempMold;

    public int spawnInterval = 0;

    private void Awake()
    {
        moldPool = GetComponent<MoldPool>();
    }

    public void OnEvent_SpawnMold()
    {
        Debug.Log("mold");
        spawnInterval++;
        if (spawnInterval == 1)
        {
            StartCoroutine(MoldDelay());
        }
        if (spawnInterval > 3) 
        {
            spawnInterval = 0;
        }
        



    }

    public void OnEvent_MoveMold()
    {
        if (spawnInterval == 0)
        {
            
        }
        
    }

    public void EventCatchNote()
    {
        //spawnInterval++;
    }

    IEnumerator MoldDelay()
    {
        yield return new WaitForSeconds(4 * (60 / GameManager.Instance.BPM-0.5f));
        tempMold = moldPool.pool.Get();
        tempMold.SetPoint(standPoint);
        spawnInterval++;
        livingMolds = this.gameObject.GetComponentsInChildren<Mold>();
        foreach (Mold living in livingMolds)
        {
            if (living.isLive)
            {
                living.SetNext();
            }
        }
    }
}
    
