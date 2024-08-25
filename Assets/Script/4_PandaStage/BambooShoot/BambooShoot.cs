using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BambooShoot : MonoBehaviour
{
    Vector3 startScale = new Vector3(0.3f, 0.3f, 0.3f);
    Vector3 endScale = new Vector3(1f, 1f, 1f);
    public Transform spawnPoint;
    public Transform centerPoint;
    private float transitionMoveDuration = 0.03f;
    private float startMoveDuration = 1f;
    public bool isMoving = false;
    Animator animator;
    public SliceHand hand;
    // Start is called before the first frame update
    void Awake()
    {
        MoveBambooShoot(startMoveDuration);
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("ChopIndex", hand.sliceHandIndex);
        if (hand.finalSlice)
        {
            animator.SetTrigger("FinalSlice");
            hand.finalSlice = false;
        }

        
        
    }

    
    
    public void MoveBambooShoot(float moveDuration)
    {
        StartCoroutine(MoveCoroutine(moveDuration));
    }

    IEnumerator MoveCoroutine(float moveDuration)
    {
        isMoving = true;
        float elapsedTime = 0;
        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(spawnPoint.position, centerPoint.position, elapsedTime / moveDuration);
            transform.localScale = Vector3.Lerp(startScale, endScale, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.position = centerPoint.position;
        transform.localScale = endScale;
        isMoving = false;
    }
}
