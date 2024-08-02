using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickManager : MonoBehaviour
{
    public float rotationSpeed = 100f;
    [SerializeField] Transform rotationPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RotateStick()
    {
        StartCoroutine(RotateStickCoroutine());
    }

    IEnumerator RotateStickCoroutine()
    {
        float remainingAngle = 90f;
        while (remainingAngle > 0)
        {
            float rotationThisFrame = rotationSpeed * Time.deltaTime;
            rotationThisFrame = Mathf.Min(rotationThisFrame, remainingAngle);
            transform.RotateAround(rotationPoint.position, Vector3.forward, rotationThisFrame);
            remainingAngle -= rotationThisFrame;
            yield return null;
        }
    }
}
