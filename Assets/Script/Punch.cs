using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    float punchDuration = 0.1f;
    float upDuration = 0.2f;
    float elapsedTime = 0f;
    Vector3 startPos;
    Animator animator;
    // Start is called before the first frame update
    private void Start()
    {
        animator = GetComponent<Animator>();
        startPos = transform.position;
    }
    public void OnEvent_Punch()
    {
        StartCoroutine(PunchSuccess());
        
    }

    IEnumerator PunchSuccess()
    {
        elapsedTime = 0f;
        while (elapsedTime < punchDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, startPos + new Vector3(0, -8.5f, 0), elapsedTime / punchDuration);
            yield return null;
        }
        animator.SetTrigger("Punch");
        yield return new WaitForSeconds(0.2f);
        elapsedTime = 0f;
        while (elapsedTime < upDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos + new Vector3(0, -8.5f, 0), startPos, elapsedTime / upDuration);
            yield return null;
        }

    }
}
