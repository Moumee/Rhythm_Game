using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextEffectMove : MonoBehaviour
{
    public Transform[] noteCenter;

    public void EffectMove(int stage)
    {
        transform.position = noteCenter[stage].position+Vector3.down*4;
    }
}
