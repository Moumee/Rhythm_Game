using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class NotePool : MonoBehaviour
{
    public ObjectPool<Note> pool;
    [SerializeField] Note note;
    Transform noteSpawnPoint;

    private void Start()
    {
        noteSpawnPoint = FindObjectOfType<NoteManager>().noteSpawnPoint;
        pool = new ObjectPool<Note>(CreateNote, OnTakeNoteFromPool,
            OnReturnNoteToPool, OnDestroyNote, true, 10, 20);
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
