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
        if (GameManager.Instance.isStage1_2 && !stageCheck)
        {
            noteDirection = Vector3.down;

            stageCheck = true;
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
        GameObject closestNote = null;
        float minDistance = Mathf.Infinity;
        foreach (GameObject note in notesToCheck)
        {
            float distance = Vector3.Distance(note.gameObject.transform.position, new Vector3(0, 9.3f, 0));
            if (distance < minDistance)
            {
                minDistance = distance;
                closestNote = note;
            }
        }
        closestNote.GetComponent<Note>().judged = true;
        closestNote.GetComponent<Note>().animator.SetTrigger(name);
    }





}