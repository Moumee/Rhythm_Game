using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class NoteManager : MonoBehaviour
{
    [SerializeField] GameObject[] standPoint;
    public Transform noteSpawnPoint;
    public Transform noteCenterPoint;
    [SerializeField] GameObject[] Notes;
    private NotePool notePool;
    public Vector3 noteDirection;

    private GameObject[] noteTimingBoxes;

    private List<int> exampleBeats = new List<int> { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1};
    public List<GameObject> notesToCheck = new List<GameObject>();
    [SerializeField] int bpm = 105;
    int currentIndex = 0;
    private void Awake()
    {
        notePool = GetComponent<NotePool>();
    }


    public void EventNoteSpawn()
    {
        Note note = notePool.pool.Get();
        note.moveDirection = noteDirection;
    }



}