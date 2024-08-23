using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabbage : MonoBehaviour
{
    public CabbageManager manager;
    private Transform[] cabbagePoints;
    private Animator animator;
    public int positionIndex = 0;
    public int hitCount = 0;
    private float moveDuration = 0.3f;
   
    void Awake()
    {
        animator = GetComponent<Animator>();
        cabbagePoints = manager.cabbagePoints;
        
    }

    void Update()
    {
        
    }

    public void MoveNext()
    {
        StartCoroutine(MoveNextCoroutine());
    }

    private IEnumerator MoveNextCoroutine()
    {
        if (positionIndex != 4)
        {
            float elapsedTime = 0f;
            while (elapsedTime < moveDuration)
            {
                elapsedTime += Time.deltaTime;
                transform.position = Vector3.Lerp(cabbagePoints[positionIndex].position,
                    cabbagePoints[positionIndex + 1].position, elapsedTime / moveDuration);
                yield return null;
            }
            positionIndex = (positionIndex + 1) % cabbagePoints.Length;
            transform.position = cabbagePoints[positionIndex].position;
        }
        else if (positionIndex == 4)
        {
            positionIndex = (positionIndex + 1) % cabbagePoints.Length;
            transform.position= cabbagePoints[positionIndex].position;
            hitCount = 0;
            animator.SetBool("Survived", false);
            animator.SetTrigger("Reset");
        }


    }

    public void OnNoteHit()
    {
        hitCount++;
        animator.SetInteger("HitCount", hitCount);
        animator.SetTrigger("NoteHit");
    }

    
}
