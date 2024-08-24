using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomOil : MonoBehaviour
{
    public Animator mushroomAnim;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (mushroomAnim.GetCurrentAnimatorStateInfo(0).IsTag("Idle"))
        {
            spriteRenderer.enabled = true;
        }
        else
        {
            spriteRenderer.enabled = false;  
        }
    }
}
