using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tangerine : MonoBehaviour
{
    private Animator animator;
    public int sunHitCount = 0;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            sunHitCount++;
            animator.SetInteger("SunHitCount", sunHitCount);
        }

        
    }
}
