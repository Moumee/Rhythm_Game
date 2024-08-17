using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

abstract public class EventAdapter : MonoBehaviour
{
    //private enum stageState { ingredient = 0, mold = 1 };
    abstract public void Event_OnBeat();
    abstract public void Event_OnNote();
    abstract public void Event_CatchNote();
    abstract public void Event_MissNote();
}
