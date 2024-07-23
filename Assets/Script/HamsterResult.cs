using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using static System.Net.WebRequestMethods;

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
    public bool hasEffect = false;
    float fadeDuration = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PressAnyKey());
        if (!isAngry)
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.successEffect);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.success);
        }
        else
        {
            AudioManager.Instance.PlaySFX(AudioManager.Instance.fail);
        }

        if (!hasEffect)
        {
            effect = null;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (continueTextObj.activeInHierarchy)
        {
            if (Input.anyKey)
            {
                if (!Input.GetKey(KeyCode.Escape) && !Input.GetMouseButton(0) && !Input.GetMouseButton(1))
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
                        if (hasEffect)
                        {
                            StartCoroutine(EffectFadeOut());
                        }
                    }
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

    private IEnumerator EffectFadeOut()
    {
        float elapsedTime = 0;
        Color originalEffectColor = effect.color;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
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
