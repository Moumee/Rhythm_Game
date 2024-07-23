using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] private Transform[] knifePoints;
    [SerializeField] private float cutSpeed = 10f;
    [SerializeField] private float readySpeed = 60f;
    [SerializeField] private float horizontalSpeed = 6f;
    [SerializeField] private float cutDepth = 0.6f;
    [SerializeField] private float resetDepth = 10f;

    private int knifeIndex = 0;
    private bool isMoving = false;
    private Vector3 targetPosition;
    private Vector3 startPosition;
    private KnifeState currentState = KnifeState.Resetting;

    private enum KnifeState
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
            case KnifeState.Ready:
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    StartKnifeMovement();
                }
                break;
            case KnifeState.MovingDown:
            case KnifeState.MovingUp:
            case KnifeState.MovingHorizontal:
            case KnifeState.MovingDownToReset:
            case KnifeState.Resetting:
                MoveKnife();
                break;
        }
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
        if (knifeIndex == knifePoints.Length - 1 && currentState == KnifeState.Ready)
        {
            // At the last point, start moving down to reset
            currentState = KnifeState.MovingDownToReset;
            startPosition = transform.position;
            targetPosition = new Vector3(startPosition.x, startPosition.y - resetDepth, startPosition.z);
        }
        else
        {
            currentState = KnifeState.MovingDown;
            startPosition = transform.position;
            targetPosition = startPosition + Vector3.down * cutDepth;
        }
    }

    private void MoveKnife()
    {
        switch (currentState)
        {
            case KnifeState.MovingDown:
                MoveTowardsTarget(targetPosition, cutSpeed, () =>
                {
                    currentState = KnifeState.MovingUp;
                    targetPosition = startPosition;
                });
                break;

            case KnifeState.MovingUp:
                MoveTowardsTarget(targetPosition, cutSpeed, () =>
                {
                    currentState = KnifeState.MovingHorizontal;
                    knifeIndex = (knifeIndex + 1) % knifePoints.Length;
                    targetPosition = knifePoints[knifeIndex].position;
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
                    // Teleport to the position below the first knife point
                    Vector3 firstKnifePoint = knifePoints[0].position;
                    transform.position = new Vector3(firstKnifePoint.x, firstKnifePoint.y - resetDepth, firstKnifePoint.z);
                    currentState = KnifeState.Resetting;
                    targetPosition = knifePoints[0].position;
                });
                break;

            case KnifeState.Resetting:
                MoveTowardsTarget(targetPosition, readySpeed, () =>
                {
                    currentState = KnifeState.Ready;
                    isMoving = false;
                    knifeIndex = 0;
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