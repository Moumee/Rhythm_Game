using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reply : MonoBehaviour
{
    private Vector3 moveDistance = new Vector3 (-48f, 182f, 0f);
    private Image sprite;
    // Start is called before the first frame update
    void Awake()
    {
        sprite = GetComponent<Image>();
        sprite.CrossFadeAlpha(0f, 0f, true);
    }

    public void Apear()
    {

    }

    public void Slide()
    {

    }
}
