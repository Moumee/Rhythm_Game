using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NoteManager : MonoBehaviour
{
    [SerializeField] GameObject[] standPoint;
    [SerializeField] GameObject[] Notes;

    enum noteType { seed, bisket }


    public void SpawnNote()
    {
        GameObject instance = Instantiate(Notes[0]);
    }
}