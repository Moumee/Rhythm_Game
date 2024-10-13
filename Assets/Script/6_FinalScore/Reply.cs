using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Reply : MonoBehaviour
{
    private Vector3 moveDistance = new Vector3(-48f, 182f, 0f);
    private int position;
    private Image sprite;
    private Vector3 destination;
    private RectTransform rect;

    private bool isApear = false, isSlide = false;

    // Start is called before the first frame update
    void Awake()
    {
        sprite = GetComponent<Image>();
        rect = GetComponent<RectTransform>();
        sprite.CrossFadeAlpha(0f, 0f, true);
    }

    public void initialize(Sprite spr, int postionNum)
    {
        sprite.sprite = spr;
        rect.position += (- postionNum) * moveDistance;
        destination = rect.position;
    }

    public void Apear()
    {  
        sprite.CrossFadeAlpha(1f, 0.5f, false);
        destination = rect.position;
        rect.position -= moveDistance.normalized * 50;
    }


    public void Slide()
    {
        destination = rect.position + moveDistance;
    }

    public void Update()
    {
        if ((rect.position - destination).magnitude > 1f)
        {
            rect.position += moveDistance.normalized * 700 * Time.deltaTime;
        }
        else
        {
            rect.position = destination;
        }
        
    }
}
