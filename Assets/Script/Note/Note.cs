using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;
using static GameManager;

public class Note : MonoBehaviour
{
    private ObjectPool<Note> _pool;
    public float speed;
    public Vector3 moveDirection;
    public bool judged = false;
    public Animator animator;
    private NoteManager noteManager;

    public bool isOnTime;

    public int serialnum;

    //for test
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        noteManager = FindObjectOfType<NoteManager>();
        animator = GetComponent<Animator>();

        speed = 14 / (GameManager.Instance.noteBeatInterval * BeatTracker.GetBeatInterval());

    }

    private void OnEnable()
    {
        float interval = 60 / GameManager.Instance.BPM;
        judged = false;
        
        serialnum = GameManager.Instance.noteSerialNum;
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

        if (serialnum == GameManager.Instance.judgeNumber && GameManager.Instance.currentState != catchState.Miss && !judged)
        { 
            isOnTime = true;
        }
        else
        {
            isOnTime = false;
        }

        if ((transform.position.x <= 0 && moveDirection == Vector3.left) || (transform.position.y <= 0 && moveDirection == Vector3.down))
        {
            StartCoroutine(Dead());
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

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(0.4f);
        judged = true;
        animator.SetTrigger("Miss");
    }
}
