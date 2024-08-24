using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabbageFist : MonoBehaviour
{
    Animator animator;
    public CabbageManager manager;
    Cabbage currentCabbage;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        currentCabbage = manager.currentCabbage;
        
    }

    public void OnNoteMiss()
    {
        animator.SetTrigger("Miss");
    }

    public void OnNoteHit()
    {
        animator.SetTrigger("Hit");
        animator.SetInteger("HitIndex", currentCabbage.hitCount);
    }

    
}
