using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HamsterResult : MonoBehaviour
{
    float objectAlpha;
    [SerializeField] GameObject continueTextObj;
    [SerializeField] SpriteRenderer speechBubble;
    [SerializeField] SpriteRenderer effect;
    [SerializeField] Animator hamsterAnim;

    float fadeDuration = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PressAnyKey());
    }

    // Update is called once per frame
    void Update()
    {

        if (continueTextObj.activeInHierarchy)
        {
            if (Input.anyKey)
            {
                continueTextObj.SetActive(false);
                hamsterAnim.SetTrigger("Down");
                StartCoroutine(FadeOut());
            }
        }
        
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0;
        Color originalSpeechBubbleColor = speechBubble.color;
        Color originalEffectColor = effect.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            speechBubble.color = new Color(originalSpeechBubbleColor.r, originalSpeechBubbleColor.g, originalSpeechBubbleColor.b, alpha);
            effect.color = new Color(originalEffectColor.r, originalEffectColor.g, originalEffectColor.b, alpha);
            yield return null;
        }

        speechBubble.color = new Color(originalSpeechBubbleColor.r, originalSpeechBubbleColor.g, originalSpeechBubbleColor.b, 0);
        effect.color = new Color(originalEffectColor.r, originalEffectColor.g, originalEffectColor.b, 0);
    }

    IEnumerator PressAnyKey()
    {
        yield return new WaitForSeconds(4f);
        continueTextObj.SetActive(true);
    }
}
