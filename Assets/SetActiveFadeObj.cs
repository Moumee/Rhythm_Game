using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveFadeObj : MonoBehaviour
{
    
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
