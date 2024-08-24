using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    Animator animator;
    public Animator fireAnim;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Test Code

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnRightNoteHit();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            OnLeftNoteHit();
        }
    }

    public void OnPerfectRightNoteHit()
    {
        animator.SetTrigger("Right");
        fireAnim.SetTrigger("Perfect");
    }

    public void OnRightNoteHit()
    {
        animator.SetTrigger("Right");
    }

    public void OnPerfectLeftNoteHit()
    {
        animator.SetTrigger("Right");
        fireAnim.SetTrigger("Perfect");
    }

    public void OnLeftNoteHit()
    {
        animator.SetTrigger("Left");
    }

    //Executed when 20 beats have elapsed in 5-3 stage.
    public void CookMushroom()
    {
        animator.SetTrigger("Cooked");
    }

    //Executed when user hits 18 note misses after 20 beats in 5-3 stage.
    public void BurnMushroom()
    {
        animator.SetTrigger("Burnt");
    }
}
