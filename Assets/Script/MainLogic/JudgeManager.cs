using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class JudgeManager : MonoBehaviour
{
    public Transform[] noteCenterPoints;

    private void Awake()
    {
        
    }

    public void OnCollisionstay2D(Collision2D collision)
    {
        Debug.Log("fff");
        if (Input.GetKeyDown(KeyCode.DownArrow)){
            Note tempNote = collision.gameObject.GetComponent<Note>();
            tempNote.animator.SetTrigger("Perfect");
        }
    }
}
