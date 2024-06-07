using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UIElements;

public class IngredientPool : MonoBehaviour
{
    public ObjectPool<Ingredient> pool;
    [SerializeField] Ingredient ingre;
    [SerializeField] GameObject startPos;

    private void Awake()
    {
        pool = new ObjectPool<Ingredient>(CreateNote, OnTakeNoteFromPool,
            OnReturnNoteToPool, OnDestroyNote, true, 10, 20);
    }

    private Ingredient CreateNote()
    {
        
        Ingredient newNote = Instantiate(ingre, this.transform);
        newNote.SetPool(pool);

        return newNote;
    }

    private void OnTakeNoteFromPool(Ingredient _note)
    {
        transform.position = startPos.transform.position;
        _note.gameObject.SetActive(true);
    }

    private void OnReturnNoteToPool(Ingredient _note)
    {
        transform.position = startPos.transform.position;
        _note.gameObject.SetActive(false);
    }

    private void OnDestroyNote(Ingredient _note)
    {
        
        Destroy(_note.gameObject);
    }
}
