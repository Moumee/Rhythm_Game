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

    private void Awake()
    {
        moldPool = GetComponent<MoldPool>();
    }

    public void OnEvent_SpawnMold()
    {
        
        tempMold = moldPool.pool.Get();
        tempMold.SetPoint(standPoint);
    }

    public void OnEvent_MoveMold()
    {
        Debug.Log("f");
        foreach (Mold living in livingMolds)
        {
            if (living.isLive)
            {
                living.Event_BeatCall();
            }
        }
    }

    public void OnEvent_CatchNote()
    {
        livingMolds = this.gameObject.GetComponentsInChildren<Mold>();
        foreach (Mold living in livingMolds)
        {
            if (living.isLive)
            {
                //living.SetNext();
            }
        }
    }

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
