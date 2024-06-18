using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class HamsterResult : MonoBehaviour
{
    float objectAlpha;
    [SerializeField] GameObject continueTextObj;
    [SerializeField] SpriteRenderer speechBubble;
    [SerializeField] SpriteRenderer effect;
    [SerializeField] Animator hamsterAnim;
    [SerializeField] Animator speechAnim;
    private int anyKeyIndex = 0;
    [SerializeField] SpriteRenderer foodSpriteRenderer;
    [SerializeField] AudioManager.SFX sfx;
    [SerializeField] AudioManager.SFX effectSfx;
    public float videoFadeDuration = 1f;
    public bool isAngry = true;
    float fadeDuration = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PressAnyKey());
        if (!isAngry)
        {
            AudioManager.Instance.PlaySFX(effectSfx);
        }
        AudioManager.Instance.PlaySFX(sfx);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (continueTextObj.activeInHierarchy)
        {
            if (Input.anyKey && !Input.GetKey(KeyCode.Escape))
            {
                if (anyKeyIndex == 0)
                {
                    StartCoroutine(FoodFade(videoFadeDuration));
                    continueTextObj.SetActive(false);
                    StartCoroutine(PressAnyKey());
                    anyKeyIndex++;
                }
                else if (anyKeyIndex == 1)
                {
                    continueTextObj.SetActive(false);
                    hamsterAnim.SetTrigger("Down");
                    StartCoroutine(ObjectFadeOut());
                }
            }
            
        }
        
    }

    IEnumerator FoodFade(float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            Color originColor = new Color(1f, 1f, 1f, 1f);
            Color targerColor = new Color(1f, 1f, 1f, 0f);
            foodSpriteRenderer.color = Color.Lerp(originColor, targerColor, elapsedTime / duration);
            yield return null;
        }
        speechAnim.SetTrigger("Speak");
    }


    private IEnumerator ObjectFadeOut()
    {
        float elapsedTime = 0;
        Color originalSpeechBubbleColor = speechBubble.color;
        Color originalEffectColor = effect.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            speechBubble.material.color = new Color(originalSpeechBubbleColor.r, originalSpeechBubbleColor.g, originalSpeechBubbleColor.b, alpha);
            effect.material.color = new Color(originalEffectColor.r, originalEffectColor.g, originalEffectColor.b, alpha);
            yield return null;
        }

    }

    IEnumerator PressAnyKey()
    {
        yield return new WaitForSeconds(4f);
        continueTextObj.SetActive(true);
    }
}
