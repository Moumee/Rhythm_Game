using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Slap : MonoBehaviour
{

    void OnAnimationEnd()
    {
        gameObject.SetActive(false);
    }
}
