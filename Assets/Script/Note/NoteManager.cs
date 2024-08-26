using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class NoteManager : MonoBehaviour
{
    public Transform[] noteSpawnPoint;
    //[SerializeField] GameObject[] Notes;
    private NotePool notePool;
    public Vector3 noteDirection;

    private Vector3[] directionList = new Vector3[4] {Vector3.left, Vector3.right, Vector3.up, Vector3.down };

    private float[] rotationList = new float[4] { 0f, 90f, 180f, 270f };
    

    //private bool stageCheck = false;

    public List<GameObject> notesToCheck = new List<GameObject>();
    private void Awake()
    {
        notePool = GetComponent<NotePool>();
        notePool.noteSpawnPoint = noteSpawnPoint[0];


    }

    public void DirectionChange(int dir)
    {
        noteDirection = directionList[dir];
        
    }

    public void spawnPointChange(int sequence)
    {
        notePool.noteSpawnPoint = noteSpawnPoint[sequence];
    }

    private void Update()
    {
    }



    public void EventNoteSpawn(int random)
    {
        Note note = notePool.pool.Get();
        notesToCheck.Add(note.gameObject);
        note.moveDirection = noteDirection;

        note.transform.rotation = Quaternion.Euler(Vector3.forward * rotationList[random]);
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
            if (name == "Miss")
            {
                notesToCheck.Remove(note);
            }
        }
    }





}