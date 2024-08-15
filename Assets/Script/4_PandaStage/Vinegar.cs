using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vinegar : MonoBehaviour
{
    public SeasoningManager manager;
    Animator animator;
    State currentState = State.Idle;
    public int pointIndex = 0;
    private float moveDuration = 0.1f;
    public enum State
    {
        Idle,
        Moving,
    }
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveNext()
    {
        if (pointIndex != manager.wayPoints.Length - 1)
        {
            pointIndex = (pointIndex + 1) % manager.wayPoints.Length;
            Vector3 nextTarget = manager.wayPoints[pointIndex].position;
            StartCoroutine(MoveCoroutine(nextTarget));
        }
        else
        {
            pointIndex = 0;
            transform.position = manager.wayPoints[pointIndex].position;
        }

    }

    IEnumerator MoveCoroutine(Vector3 nextTarget)
    {
        currentState = State.Moving;
        float timer = 0f;
        while (timer < moveDuration)
        {
            transform.position = Vector3.Lerp(transform.position, nextTarget, timer / moveDuration);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position = nextTarget;
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
