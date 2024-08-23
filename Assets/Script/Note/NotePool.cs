using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class NotePool : MonoBehaviour
{
    public ObjectPool<Note> pool;
    [SerializeField] Note note;
    public Transform noteSpawnPoint;

    private void Awake()
    {
        //noteSpawnPoint = GetComponent<Transform>();
        //noteSpawnPoint = FindObjectOfType<NoteManager>().noteSpawnPoint;
        pool = new ObjectPool<Note>(CreateNote, OnTakeNoteFromPool,
            OnReturnNoteToPool, OnDestroyNote, false, 4, 20);
    }

    private Note CreateNote()
    {
        Note newNote = Instantiate(note, noteSpawnPoint.position, Quaternion.identity);
        newNote.SetPool(pool);

        return newNote; 
    }

    private void OnTakeNoteFromPool(Note _note)
    {
        _note.transform.position = noteSpawnPoint.position;

        _note.gameObject.SetActive(true);
    }

    private void OnReturnNoteToPool(Note _note)
    {
        _note.gameObject.SetActive(false);  
    }

    private void OnDestroyNote(Note _note)
    {
        Destroy(_note.gameObject);
    }
}
