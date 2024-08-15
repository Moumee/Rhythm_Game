using FMOD;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Seasoning : MonoBehaviour
{
    public delegate void OnMove();
    public static event OnMove onMove;

    public SeasoningManager manager;
    public Animator animator;
    State currentState = State.Idle;
    public int pointIndex = 0;
    private float moveDuration = 0.3f;
    public bool keyPressed = false;
    float checkDuration = 0.5f;

    public enum State
    {
        Idle,
        Moving,
    }
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        if (pointIndex == 0)
        {
            manager.readySeasonings.Add(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pointIndex == 2)
        {
            pointIndex = 0;
            transform.position = manager.wayPoints[0].position;
            manager.readySeasonings.Add(this);
            animator.SetTrigger("Reset");
        }

        

        
    }

    IEnumerator CheckKeyPress()
    {
        yield return new WaitForSeconds(0.1f);
        float timer = 0;
        while (timer < checkDuration)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.UpArrow) || 
                Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                keyPressed = true;
            }

            timer += Time.deltaTime;

            yield return null;
        }
        if (!keyPressed && manager.currentSeasoning == this)
        {
            onMove?.Invoke();
            StartCoroutine(MoveCoroutine());
        }
        keyPressed = false;
    }

    public void MoveNextCheck()
    {
        
        StartCoroutine(CheckKeyPress());
    }

    public void MoveNext()
    {
        StartCoroutine(MoveCoroutine());
    }

    public IEnumerator MoveCoroutine()
    {
        int nextIndex = pointIndex + 1;
        if (nextIndex != 3)
        {
            Vector3 nextTarget = manager.wayPoints[nextIndex].position;
            currentState = State.Moving;
            float timer = 0f;
            while (timer < moveDuration)
            {
                transform.position = Vector3.Lerp(transform.position, nextTarget, timer / moveDuration);
                timer += Time.deltaTime;
                yield return null;
            }
            transform.position = nextTarget;
        }
        pointIndex = nextIndex;
        currentState = State.Idle;
    }

    public void OnNoteHit()
    {
        if (currentState == State.Idle)
        {
            animator.SetBool("NoteHit", true);
            animator.SetTrigger("Down");
        }
        
    }

    public void OnNoteMiss()
    {
        if (currentState == State.Idle)
        {
            animator.SetBool("NoteHit", false);
            animator.SetTrigger("Down");
        }
    }
}
