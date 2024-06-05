using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] RectTransform canvasRectTransform;
    RectTransform eyeRectTransform;
    [SerializeField] RectTransform centerRectTransform;
    [SerializeField] float eyeMovementRadius;
    [SerializeField] float minDistance;

    private void Start()
    {
        eyeRectTransform = GetComponent<RectTransform>();
    }
    void Update()
    {
        Vector3 mouseCanvasPosition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRectTransform, Input.mousePosition, null, out mouseCanvasPosition);

        Vector3 direction = (mouseCanvasPosition - eyeRectTransform.position).normalized;
        direction.z = 0f;
        
        float distance = Vector3.Distance(mouseCanvasPosition, eyeRectTransform.position);
        if (distance > minDistance)
        {
            Vector3 targetPosition = centerRectTransform.position + direction * Mathf.Min(eyeMovementRadius, distance);
            eyeRectTransform.position = new Vector3(targetPosition.x, targetPosition.y, eyeRectTransform.position.z);
        }
    }
}