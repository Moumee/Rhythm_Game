using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCheese : MonoBehaviour
{
    public SpriteRenderer[] cheeseRenderers;
    private float appearDuration = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FirstPile()
    {
        StartCoroutine(CheeseAppear(0));
    }

    public void SecondPile()
    {
        StartCoroutine(CheeseAppear(1));
    }

    public void ThirdPile()
    {
        StartCoroutine((CheeseAppear(2)));
    }

    public void FourthPile()
    {
        StartCoroutine(CheeseAppear((3)));
    }

    IEnumerator CheeseAppear(int index)
    {
        Color startColor = cheeseRenderers[index].color;
        Color targetColor = new Color(1, 1, 1, 1);
        float timer = 0f;
        while (appearDuration > timer)
        {
            cheeseRenderers[index].color = Color.Lerp(startColor, targetColor, timer / appearDuration);
            yield return null;
        }
        cheeseRenderers[index].color = targetColor;
    }
}
