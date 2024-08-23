using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabbageFist : MonoBehaviour
{
    Animator animator;
    public int hitIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    OnNoteHit();
        //}
    }

    public void OnNoteMiss()
    {
        animator.SetTrigger("Miss");
    }

    public void OnNoteHit()
    {
        animator.SetTrigger("Hit");
        animator.SetInteger("HitIndex", hitIndex);
        hitIndex = (hitIndex + 1) % 3;
    }

    
}
