using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScale : MonoBehaviour
{
    [SerializeField] private float buttonScale = 1.2f;
    Vector2 defaultScale;
    private void Start()
    {
        defaultScale = transform.localScale;
    }
    public void PointerEnter()
    {
        transform.localScale = new Vector2(buttonScale, buttonScale);
    }

    public void PointerExit()
    {
        transform.localScale = defaultScale;
    }
}
