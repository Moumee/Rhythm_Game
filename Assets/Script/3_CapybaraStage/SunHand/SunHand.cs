using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class SunHand : MonoBehaviour
{
    public EventReference sunSqueeze;
    public float moveSpeed;
    private bool sunHandAppeared = false;
    private Animator animator;
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!sunHandAppeared)
        {
            SunHandAppear();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            AudioManager.Instance.PlaySFX(sunSqueeze);
            animator.SetTrigger("Squeeze");
        }
    }

    void SunHandAppear()
    {
        if (transform.position.x > 0)
        {
            transform.position -= new Vector3(moveSpeed, 0, 0) * Time.deltaTime;
        }
        else
        {
            sunHandAppeared = true;
            animator.SetBool("Appeared", true);
            transform.position = Vector3.zero;
        }
    }
}
