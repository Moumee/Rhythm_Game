using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalResultCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(CursorEnableCoroutine());
    }

    IEnumerator CursorEnableCoroutine()
    {
        yield return new WaitForSeconds(3.3f);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
