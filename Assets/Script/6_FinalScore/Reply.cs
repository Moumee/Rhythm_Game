using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reply : MonoBehaviour
{
    private Vector3 moveDistance = new Vector3(-48f, 182f, 0f);
    private int position;
    private Image sprite;
    private Vector3 destination;
    private RectTransform rect;

    // Start is called before the first frame update
    void Awake()
    {
        sprite = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        sprite.color = new Color(1f, 1f, 1f, 0f);
    }

    public void Initialize(Sprite spr, int positionNum)
    {
        sprite.sprite = spr;
        // Set localPosition instead of position
        rect.localPosition += (-positionNum) * moveDistance;
        destination = rect.localPosition; // Use localPosition for destination
    }

    public void Appear()
    {
        StartCoroutine(AlphaCoroutine(1f, 0.5f));
        destination = rect.localPosition; // Update destination
        rect.localPosition -= moveDistance.normalized * 50; // Use localPosition
    }

    IEnumerator AlphaCoroutine(float targetAlpha, float duration)
    {
        
        Color targetColor = sprite.color;
        Color startColor = sprite.color;
        targetColor.a = targetAlpha;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            sprite.color = Color.Lerp(startColor, targetColor,elapsedTime / duration);
            yield return null;
        }
        sprite.color = targetColor;
        
    }

    public void Slide()
    {
        if (rect.localPosition.y >= 722f) // Use localPosition for comparison
        {
            StartCoroutine(AlphaCoroutine(0f, 0.3f));
        }

        destination = rect.localPosition + moveDistance; // Update destination
    }
    
    

    public void Update()
    {
        // Check distance using localPosition
        if ((rect.localPosition - destination).magnitude > 5f)
        {
            rect.localPosition += moveDistance.normalized * 700f * Time.deltaTime; // Use localPosition
        }
        else
        {
            rect.localPosition = destination; // Ensure we set to destination
        }

    }
}