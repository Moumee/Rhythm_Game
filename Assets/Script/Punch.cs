using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    // Start is called before the first frame update
    public void OnEvent_Punch()
    {
        StartCoroutine(PunchSuccess());
        
    }

    IEnumerator PunchSuccess()
    {
        transform.position += Vector3.down * 8;
        yield return new WaitForSeconds(0.5f);
        transform.position -= Vector3.down * 8;
    }
}
