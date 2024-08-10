using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : MonoBehaviour
{
    float punchDuration = 0.1f;
    float upDuration = 0.2f;   
    float missDuration = 0.05f;
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

    public void OnEvent_Miss()
    {
        StartCoroutine(PunchMiss());    
    }

    IEnumerator PunchSuccess()
    {
        float elapsedTime = 0f;
        while (elapsedTime < punchDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, startPos + new Vector3(0, -8.5f, 0), elapsedTime / punchDuration);
            yield return null;
        }
        transform.position = startPos + new Vector3(0, -8.5f, 0);
        animator.SetTrigger("Punch");
        yield return new WaitForSeconds(0.2f);
        float secondElapsedTime = 0f;
        while (secondElapsedTime < upDuration)
        {
            secondElapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos + new Vector3(0, -8.5f, 0), startPos, secondElapsedTime / upDuration);
            yield return null;
        }
        transform.position = startPos;
    }

    IEnumerator PunchMiss()
    {
        float elapsedTime = 0f;
        while (elapsedTime < missDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, startPos + new Vector3(0, -3f, 0), elapsedTime / missDuration);
            yield return null;
        }
        transform.position = startPos + new Vector3(0, -3f, 0);
        yield return new WaitForSeconds(0.2f);
        float secondElapseTime = 0f;
        while (secondElapseTime < missDuration)
        {
            secondElapseTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos + new Vector3(0, -3f, 0), startPos, secondElapseTime / missDuration * 1.5f);
            yield return null;
        }
        transform.position = startPos;
    }
}
