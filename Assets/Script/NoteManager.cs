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

    private List<int> exampleBeats = new List<int> { 1, 1, 0, 0, 1, 0,1,1,0,0, 0, 1,0,1,0,1,0,1,0, 1 };
    public List<GameObject> notesToCheck = new List<GameObject>();
    [SerializeField] int bpm = 105;
    int currentIndex = 0;

    double timer = 0d;
    enum noteType { seed, cracker }
    private void Awake()
    {
        notePool = GetComponent<NotePool>();
    }

    private void Start()
    {
        
        StartCoroutine(IterateBeats());


    }
    private void Update()
    {
        timer += Time.deltaTime;
        
    }

    private IEnumerator IterateBeats()
    {
        float beatInterval = 60f / bpm;
        while (true) 
        {
            for (int i = 0; i < exampleBeats.Count; i++)
            {
                currentIndex = i;

                if (exampleBeats[currentIndex] == 1)
                {
                    Note note = notePool.pool.Get();
                    note.moveDirection = noteDirection;
                    notesToCheck.Add(note.gameObject);

                }

                yield return new WaitForSeconds(beatInterval);
            }
        }
    }

    private void CheckTiming()
    {
        for (int i = 0; i < noteTimingBoxes.Length; i++)
        {

        }
    }


}