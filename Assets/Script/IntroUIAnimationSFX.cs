using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class IntroUIAnimationSFX : MonoBehaviour
{
    public bool isChef = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayUISFX()
    {
        if (isChef)
        {
            RuntimeManager.PlayOneShot("event:/Woosh1");
        }
        else
        {
            RuntimeManager.PlayOneShot("event:/Woosh2");
        }
    }
}
