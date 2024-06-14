using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class Note : MonoBehaviour
{
    private ObjectPool<Note> _pool;
    private float speed;
    Transform noteSpawnPoint;
    public Vector3 moveDirection;

    // Start is called before the first frame update
    void Start()
    {
        noteSpawnPoint = FindObjectOfType<NoteManager>().noteSpawnPoint;
        speed = 14/(4*60/GameManager.Instance.BPM)+2f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDirection * speed * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("NoteEnd"))
        {
            FindObjectOfType<NoteManager>().notesToCheck.Remove(gameObject);
            _pool.Release(this);
        }
        if (collision.CompareTag("NoteOut"))
        {
            FindObjectOfType<NoteManager>().notesToCheck.Remove(gameObject);
        }
    }
    public void SetPool(ObjectPool<Note> pool)
    {
        _pool = pool;
    }
}
