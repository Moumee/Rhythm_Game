using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class ButtonEffect : MonoBehaviour
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

    public void PlayClick()
    {
        //AudioManager.Instance.PlaySFX(AudioManager.SFX);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
    }

    public void PlayStart()
    {
        //AudioManager.Instance.PlaySFX(startSound);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.start);
    }
}
