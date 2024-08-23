using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class Note : MonoBehaviour
{
    private ObjectPool<Note> _pool;
    public float speed;
    public Vector3 moveDirection;
    public bool judged = false;
    public Animator animator;
    private NoteManager noteManager;

    private float catchableTime;
    public bool isOnTime;

    private int serialnum;

    // Start is called before the first frame update
    void Awake()
    {
        noteManager = FindObjectOfType<NoteManager>();
        animator = GetComponent<Animator>();

        speed = 14 / (5 * BeatTracker.GetBeatInterval());

    }

    private void OnEnable()
    {
        float interval = 60 / GameManager.Instance.BPM;
        judged = false;
        catchableTime = BeatTracker.GetCurrentTime() + 4 * interval;
        serialnum = GameManager.Instance.noteNumber2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!judged)
        {
            transform.position += moveDirection * speed * Time.fixedDeltaTime;
            if (transform.position.x < -11 || transform.position.y < -7)
            {
                noteManager.notesToCheck.Remove(this.gameObject);
                _pool.Release(this);
            }
        }

        if (serialnum == GameManager.Instance.judgeNumber)
        { 
            isOnTime = true;
        }
        else
        {
            isOnTime = false;
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
