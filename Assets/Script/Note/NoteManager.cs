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
    public Transform secondSpawnPoint;
    public Vector3 noteDirection;

    private bool stageCheck = false;

    public List<GameObject> notesToCheck = new List<GameObject>();
    private void Awake()
    {
        notePool = GetComponent<NotePool>();
    }


    private void Update()
    {
        if (GameManager.Instance.isStage1_2 == 1 && !stageCheck)
        {
            noteDirection = Vector3.down;
            notePool.noteSpawnPoint = secondSpawnPoint;
            stageCheck = true;
        }
        else if (GameManager.Instance.isStage1_2==2)
        {
            noteDirection = Vector3.left;
            notePool.noteSpawnPoint = noteSpawnPoint;
        }
    }

    public void EventNoteSpawn()
    {
        Note note = notePool.pool.Get();
        notesToCheck.Add(note.gameObject);
        note.moveDirection = noteDirection;
    }

    public void EventCatchNote()
    {

    }

    public void NoteJudgeEffect(string name)
    {
        foreach (GameObject note in notesToCheck)
        {
            Note tempNote = note.GetComponent<Note>();
            if (tempNote.isOnTime)
            {
                tempNote.judged = true;
                tempNote.animator.SetTrigger(name);
            }
        }
    }





}