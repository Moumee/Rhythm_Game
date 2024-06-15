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

        if (spawnInterval == 0)
        {
            tempMold = moldPool.pool.Get();
            tempMold.SetPoint(standPoint);
            livingMolds = this.gameObject.GetComponentsInChildren<Mold>();
            foreach (Mold living in livingMolds)
            {
                if (living.isLive)
                {
                    living.SetNext();
                }
            }
        }
        spawnInterval++;
        if (spawnInterval > 2)
        {
            spawnInterval = 0;
        }

        
    }

    public void OnEvent_MoveMold()
    {
        
    }

    //public void OnEvent_CatchNote()
    //{
    //    livingMolds = this.gameObject.GetComponentsInChildren<Mold>();
    //    foreach (Mold living in livingMolds)
    //    {
    //        if (living.isLive)
    //        {
    //            //living.SetNext();
    //        }
    //    }
    //}

    public void EventCatchNote()
    {
        livingMolds2 = this.gameObject.GetComponentsInChildren<Mold>();
        foreach (Mold living in livingMolds2)
        {
            if (living.isOnTime)
            {
            }
        }
    }
}
