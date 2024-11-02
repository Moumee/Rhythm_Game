using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnableFinger : MonoBehaviour
{
    [SerializeField] private Image fingerImage;

    public void ShowFinger()
    {
        fingerImage.color = Color.white;
    }
}
