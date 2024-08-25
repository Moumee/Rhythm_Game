using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseGrater : MonoBehaviour
{
    public EventReference grateCheeseSFX;
    Animator animator;
    public Animator cheeseAnim;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Test Code
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            OnGoodNoteHit();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            OnPerfectNoteHit();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            OnNoteMiss();
        }
    }

    public void OnGoodNoteHit()
    {
        AudioManager.Instance.PlaySFX(grateCheeseSFX);
        animator.SetTrigger("Hit");
        cheeseAnim.SetTrigger("Good");
    }

    public void OnPerfectNoteHit()
    {
        AudioManager.Instance.PlaySFX(grateCheeseSFX);
        animator.SetTrigger("Hit");
        cheeseAnim.SetTrigger("Perfect");
    }

    public void OnNoteMiss()
    {
        animator.SetTrigger("Miss");
    }
}
