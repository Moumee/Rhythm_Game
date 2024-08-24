using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stick : MonoBehaviour
{
    [SerializeField] private TangerineFallManger tangerineFallManger;
    public float rotationSpeed = 500f;
    public Transform rotationPoint;
    public float rotationDelay = 0.8f;
    public bool isRotating = false;
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
        isRotating = true;
        yield return new WaitForSeconds(rotationDelay);
        float remainingAngle = 180f;
        while (remainingAngle > 0)
        {
            float rotationThisFrame = rotationSpeed * Time.deltaTime;
            rotationThisFrame = Mathf.Min(rotationThisFrame, remainingAngle);
            transform.RotateAround(rotationPoint.position, Vector3.forward, rotationThisFrame);
            remainingAngle -= rotationThisFrame;
            yield return null;
        }
        
        tangerineFallManger.ResetAllTangerines();
        isRotating = false;
    }
}
