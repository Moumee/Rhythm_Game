using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cabbage : MonoBehaviour
{
    public CabbageManager manager;
    private Transform[] cabbagePoints;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public int positionIndex = 0;
    public int hitCount = 0;
    private float moveDuration = 0.2f;
    private bool firstMove = false;
    private bool invisibleStart = false;
   
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
        animator = GetComponent<Animator>();
        cabbagePoints = manager.cabbagePoints;
        

        
    }

    private void Start()
    {
        if (positionIndex == 3)
        {
            invisibleStart = true;
            spriteRenderer.enabled = false;
        }

    }

    void Update()
    {
        if (positionIndex == 4)
        {
            animator.SetTrigger("Reset");
        }
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
            positionIndex++;
            transform.position = cabbagePoints[positionIndex].position;
        }
        else if (positionIndex == 4)
        {
            positionIndex = 0;
            transform.position= cabbagePoints[positionIndex].position;
            hitCount = 0;
            animator.SetInteger("HitCount", 0);
        }
        if (!firstMove && invisibleStart)
        {
            spriteRenderer.enabled = true;
            firstMove = true;
        }
        if (positionIndex == 3 && hitCount == 0)
        {
            animator.SetTrigger("Survived");
        }

    }

    public void OnNoteHit()
    {
        hitCount++;
        animator.SetInteger("HitCount", hitCount);
        animator.SetTrigger("NoteHit");
    }

    
}
