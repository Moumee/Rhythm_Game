using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class IngredientPool : MonoBehaviour
{
    public ObjectPool<Ingredient> pool;
    [SerializeField] Ingredient note;
    Transform noteSpawnPoint;

    private void Awake()
    {
        noteSpawnPoint = FindObjectOfType<NoteManager>().noteSpawnPoint;
        pool = new ObjectPool<Ingredient>(CreateNote, OnTakeNoteFromPool,
            OnReturnNoteToPool, OnDestroyNote, true, 10, 20);
    }

    private Ingredient CreateNote()
    {
        Ingredient newNote = Instantiate(note, this.transform);
        newNote.SetPool(pool);

        return newNote;
    }

    private void OnTakeNoteFromPool(Ingredient _note)
    {
        _note.transform.position = noteSpawnPoint.position;

        _note.gameObject.SetActive(true);
    }

    private void OnReturnNoteToPool(Ingredient _note)
    {
        _note.gameObject.SetActive(false);
    }

    private void OnDestroyNote(Ingredient _note)
    {
        Destroy(_note.gameObject);
    }
}
