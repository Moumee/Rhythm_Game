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

        Vector3 direction = ((Vector2)(mouseCanvasPosition - eyeRectTransform.position));
        direction.z = 0f;
        direction = (Vector2)direction.normalized;
        float distance = Vector2.Distance(mouseCanvasPosition, eyeRectTransform.position);
        if (distance > minDistance)
        {
            Vector2 targetPosition = centerRectTransform.position + direction * eyeMovementRadius;
            eyeRectTransform.position = targetPosition;
        }
    }
}