using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishCut : MonoBehaviour
{
    public GameObject parentObject;

    public GameObject[] cutObjects;

    private void OnEnable()
    {
        cutObjects = new GameObject[parentObject.transform.childCount];

        for (int i = 0; i < cutObjects.Length; i++)
        {
            cutObjects[i] = parentObject.transform.GetChild(i).gameObject;
        }
    }
}
