using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tangerine : MonoBehaviour
{
    public Animator animator;
    public TangerineManager tangerineManager;
    public Transform[] tangerinePoints;
    public int sunHitCount = 0;
    public int currentIndex;
    private int moveOffset = 13;
    private float moveDuration = 0.3f;
    // Start is called before the first frame update
    void Awake()
    {
        tangerinePoints = tangerineManager.tangerinePoints;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {



    }

    public void MoveTangerineLeft()
    {
        StartCoroutine(MoveTangerineLeftCoroutine());
    }

    private IEnumerator MoveTangerineLeftCoroutine()
    {
        int nextIndex = (currentIndex + 1) % 3;
        float elapsedTime = 0;
        while (moveDuration >= elapsedTime)
        {
            transform.position = Vector3.Lerp(tangerinePoints[currentIndex].position, 
                tangerinePoints[nextIndex].position, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        currentIndex = nextIndex;
        transform.position = tangerinePoints[nextIndex].position;   
        if (currentIndex == 2)
        {
            transform.position = tangerinePoints[0].position;
            currentIndex = 0;
            sunHitCount = 0;
            tangerineManager.ResetMoveFlag();
            animator.SetInteger("SunHitCount", sunHitCount);
        }
        
    }
}
