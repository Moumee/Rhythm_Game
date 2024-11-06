using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Knife : MonoBehaviour
{
    [SerializeField] private FishManager fishManager;
    [SerializeField] private Transform[] knifePoints;
    [SerializeField] private float cutDuration = 0.1f;
    [SerializeField] private float horizontalDuration = 0.1f;
    [SerializeField] private float resetDuration = 0.2f;
    [SerializeField] private float cutDepth = 0.6f;
    [SerializeField] private float resetDepth = 10f;

    public int knifeIndex = 0;
    private bool isMoving = false;
    private bool sfxPlayed = false;


    public void OnKeyPress(bool passThrough)
    {
        StartCoroutine(CutCoroutine(passThrough));
    }

    private IEnumerator ResetCoroutine()
    {
        fishManager.MoveAllFish();
        Vector3 startPos = knifePoints[knifeIndex].position;
        Vector3 targetPos = startPos - resetDepth * Vector3.up;
        float elapsedTime = 0f;
        while (elapsedTime < resetDuration / 2f)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / (resetDuration / 2f));
            yield return null;
        }
        transform.position = knifePoints[0].position - resetDepth * Vector3.up;
        startPos = transform.position;
        targetPos = knifePoints[0].position;
        elapsedTime = 0f;
        while (elapsedTime < resetDuration / 2f)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, targetPos, elapsedTime / (resetDuration / 2f));
            yield return null;
        }
        transform.position = targetPos;
        knifeIndex = 0;

    }

    private IEnumerator CutCoroutine(bool passThrough)
    {
        Vector3 startPosition = knifePoints[knifeIndex].position;
        Vector3 targetPosition = startPosition - cutDepth * Vector3.up;
        isMoving = true;
        float elapsedTime = 0f;
        while (elapsedTime <= (cutDuration / 2f))
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / (cutDuration / 2f));
            yield return null;
        }
        transform.position = targetPosition;
        startPosition = knifePoints[knifeIndex].position - cutDepth * Vector3.up;
        targetPosition = knifePoints[knifeIndex].position;
        elapsedTime = 0f;
        while (elapsedTime <= (cutDuration / 2f))
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / (cutDuration / 2f));
            yield return null;
        }
        transform.position = targetPosition;
        if (passThrough)
        {
            StartCoroutine(knifeIndex + 1 < knifePoints.Length - 1 ? MoveRightCoroutine() : ResetCoroutine());
        }
    }

    private IEnumerator MoveRightCoroutine()
    {
        Vector3 startPosition = knifePoints[knifeIndex].position;
        Vector3 targetPosition = knifePoints[knifeIndex + 1].position;
        float elapsedTime = 0f;
        while (elapsedTime <= horizontalDuration)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / horizontalDuration);
            yield return null;
        }
        transform.position = targetPosition;
        knifeIndex++;
        isMoving = false;
    }

    

    
}