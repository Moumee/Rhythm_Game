using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickleContainer : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    private float increaseDuration = 0.5f;
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Color tmp = spriteRenderer.color;
        tmp.a = 0;
        spriteRenderer.color = tmp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnOneThird()
    {
        StartCoroutine(IncreaseAlpha(0.3f));
    }

    public void OnTwoThirds()
    {
        StartCoroutine(IncreaseAlpha(0.6f));
    }

    public void OnThreeThirds()
    {
        StartCoroutine(IncreaseAlpha(1f));
    }

    IEnumerator IncreaseAlpha(float targetAlpha)
    {
        Color currentColor = spriteRenderer.color;
        Color targetColor = currentColor;
        targetColor.a = targetAlpha;
        float timer = 0f;
        while (increaseDuration > timer)
        {
            timer += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(currentColor, targetColor, timer / increaseDuration);
            yield return null;
        }
        spriteRenderer.color = targetColor;
    }


}
