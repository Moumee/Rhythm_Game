using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceHand : MonoBehaviour
{
    public EventReference bambooShootHit;
    Animator animator;
    SpriteRenderer spriteRenderer;
    public int sliceHandIndex = 0;
    bool cycleResetting = false;
    public int[] attemptCount = new int[6];
    public int[] noteHitCount = { 0, 0, 0, 0, 0, 0 };
    public bool finalSlice = false;
    public BambooShoot bambooShoot;
    bool isDoingFinalSlice = false;
    public GameObject parentObject;
    private Vector3 parentStartPos;
    private float timer;
    private Vector3 randomPos;
    private float shakeDuration = 0.12f;
    private float shakeDistance = 0.2f;
    private float delayBetweenShakes = 0.05f;


    // Start is called before the first frame update
    void Start()
    {
        parentStartPos = parentObject.transform.position;
        attemptCount[0] = 1;
        for (int i = 1; i < attemptCount.Length; i++)
        {
            attemptCount[i] = Random.Range(1, 3);
        }
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("slice_hand_down") && 
            !animator.GetCurrentAnimatorStateInfo(0).IsName("slice_hand_up") && 
            !animator.GetCurrentAnimatorStateInfo(0).IsName("slice_hand_idle"))
        {
            spriteRenderer.sortingOrder = 2;
        }
        else
        {
            spriteRenderer.sortingOrder = 0;
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("slice_hand_blade_6") &&
            !animator.IsInTransition(0))
        {
            isDoingFinalSlice = true;
        }
        else
        {
            isDoingFinalSlice = false;
        }

        if (cycleResetting)
        {
            for (int i = 1; i < attemptCount.Length; i++)
            {
                attemptCount[i] = Random.Range(1, 3);
            }
            cycleResetting = false;
        }

        if (sliceHandIndex == 1)
        {
            animator.SetBool("FinalSlice", false);
        }
        
        
        
    }


    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        timer = 0f;
        while (timer < shakeDuration)
        {
            randomPos = parentStartPos + (Random.insideUnitSphere * shakeDistance);
            parentObject.transform.position = randomPos;
            yield return new WaitForSeconds(delayBetweenShakes);
            timer += delayBetweenShakes;
        }

        parentObject.transform.position = parentStartPos;
    }

    public void OnNoteHit()
    {
        if (bambooShoot.isMoving || isDoingFinalSlice) return;
        AudioManager.Instance.PlaySFX(bambooShootHit);
        animator.SetBool("NoteHit", true);
        animator.SetInteger("SliceHandIndex", sliceHandIndex);
        animator.SetTrigger("Slice");

        noteHitCount[sliceHandIndex]++;
        if (noteHitCount[sliceHandIndex] == attemptCount[sliceHandIndex])
        {
            sliceHandIndex++;
            if (sliceHandIndex == 6)
            {
                finalSlice = true;
                animator.SetBool("FinalSlice", true);
                cycleResetting = true;
                for (int i = 0; i < noteHitCount.Length; i++)
                {
                    noteHitCount[i] = 0;
                }
                sliceHandIndex = 0;
                
            }
        }        
        
    }

    public void OnNoteMiss()
    {
        if (bambooShoot.isMoving || isDoingFinalSlice) return;
        animator.SetBool("NoteHit", false);
        animator.SetTrigger("Slice");
    }
    
}
