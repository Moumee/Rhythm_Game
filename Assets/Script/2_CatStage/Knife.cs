using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Knife : MonoBehaviour
{
    [SerializeField] private FishManager fishManager;
    [SerializeField] private Transform[] knifePoints;
    [SerializeField] private float cutSpeed = 20f;
    [SerializeField] private float readySpeed = 80f;
    [SerializeField] private float horizontalSpeed = 70f;
    [SerializeField] private float cutDepth = 0.6f;
    [SerializeField] private float resetDepth = 10f;

    public int knifeIndex = 0;
    private bool isMoving = false;
    private bool sfxPlayed = false;
    private Vector3 targetPosition;
    private Vector3 startPosition;
    public KnifeState currentState = KnifeState.Resetting;

    public enum KnifeState
    {
        Ready,
        MovingDown,
        MovingUp,
        MovingHorizontal,
        MovingDownToReset,
        Resetting
    }

    private void OnEnable()
    {
        ResetKnife();
    }

    private void Update()
    {
        switch (currentState)
        {
            case KnifeState.MovingDown:
            case KnifeState.MovingUp:
            case KnifeState.MovingHorizontal:
            case KnifeState.MovingDownToReset:
            case KnifeState.Resetting:
                MoveKnife();
                break;
        }
    }

    public void OnKeyPress()
    {
        if (currentState == KnifeState.Ready)
            StartKnifeMovement();
    }

    private void ResetKnife()
    {
        if (knifePoints.Length == 0)
        {
            Debug.LogError("No knife points assigned!");
            return;
        }

        Vector3 initialPosition = knifePoints[0].position;
        transform.position = new Vector3(initialPosition.x, initialPosition.y - resetDepth, initialPosition.z);
        currentState = KnifeState.Resetting;
        targetPosition = knifePoints[0].position;
        isMoving = true;
    }

    private void StartKnifeMovement()
    {
        isMoving = true;
        currentState = KnifeState.MovingDown;
        startPosition = transform.position;
        targetPosition = startPosition + Vector3.down * cutDepth;
    }

    private void MoveKnife()
    {
        switch (currentState)
        {
            case KnifeState.MovingDown:
                if (!sfxPlayed)
                {
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.knifeCut);
                    sfxPlayed = true;
                }
                MoveTowardsTarget(targetPosition, cutSpeed, () =>
                {
                    currentState = KnifeState.MovingUp;
                    targetPosition = startPosition;
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.cuttingBoard);
                    sfxPlayed = false;
                });
                break;

            case KnifeState.MovingUp:
                MoveTowardsTarget(targetPosition, cutSpeed, () =>
                {
                    if (knifeIndex == knifePoints.Length - 1)
                    {
                        currentState = KnifeState.MovingDownToReset;
                        targetPosition = new Vector3(startPosition.x, startPosition.y - resetDepth, startPosition.z);
                    }
                    else
                    {
                        currentState = KnifeState.MovingHorizontal;
                        knifeIndex = (knifeIndex + 1) % knifePoints.Length;
                        targetPosition = knifePoints[knifeIndex].position;
                    }
                });
                
                break;

            case KnifeState.MovingHorizontal:
                MoveTowardsTarget(targetPosition, horizontalSpeed, () =>
                {
                    currentState = KnifeState.Ready;
                    isMoving = false;
                });
                break;

            case KnifeState.MovingDownToReset:
                MoveTowardsTarget(targetPosition, readySpeed, () =>
                {
                    Vector3 firstKnifePoint = knifePoints[0].position;
                    transform.position = new Vector3(firstKnifePoint.x, firstKnifePoint.y - resetDepth, firstKnifePoint.z);
                    currentState = KnifeState.Resetting;
                    targetPosition = knifePoints[0].position;
                    fishManager.MoveAllFish();
                });
                break;

            case KnifeState.Resetting:
                MoveTowardsTarget(targetPosition, readySpeed, () =>
                {
                    knifeIndex = 0;
                    currentState = KnifeState.Ready;
                    isMoving = false;
                });
                break;
        }
    }

    private void MoveTowardsTarget(Vector3 target, float speed, System.Action onReachedTarget)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, target) < 0.001f)
        {
            onReachedTarget?.Invoke();
        }
    }
}