using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScale : MonoBehaviour
{
    Vector2 defaultScale;
    private void Start()
    {
        defaultScale = transform.localScale;
    }
    public void PointerEnter()
    {
        transform.localScale = new Vector2(1.2f, 1.2f);
    }

    public void PointerExit()
    {
        transform.localScale = defaultScale;
    }
}
