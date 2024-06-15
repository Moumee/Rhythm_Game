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
    public bool judged = false;
    public Animator animator;
    private NoteManager noteManager;

    // Start is called before the first frame update
    void Start()
    {
        noteManager = FindObjectOfType<NoteManager>();
        animator = GetComponent<Animator>();
        noteSpawnPoint = noteManager.noteSpawnPoint;
        speed = 14/(4*60/GameManager.Instance.BPM)+2f;
    }

    private void OnEnable()
    {
        judged = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!judged)
        {
            transform.position += moveDirection * speed * Time.deltaTime;
            if (transform.position.x < -11)
            {
                noteManager.notesToCheck.Remove(this.gameObject);
                _pool.Release(this);
            }
        }
        
        
    }
    
    public void SetPool(ObjectPool<Note> pool)
    {
        _pool = pool;
    }

    public void OnAnimationEnd()
    {
        noteManager.notesToCheck.Remove(this.gameObject);
        _pool.Release(this);
    }
}
