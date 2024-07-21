using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] Transform[] knifePoints;
    public int knifeIndex = 0;
    public float speed = 10f;
    public float readySpeed = 60f;
    private bool isMoving = false;
    private Vector3 targetPosition;
    private Vector3 startPosition;
    private bool movingDown = true;
    private bool isBeingReady = false;

    void OnEnable()
    {
        transform.position = knifePoints[0].position + Vector3.down * 10f;
        isBeingReady = true;
    }

    void Update()
    {
        if (isBeingReady)
        {
            ReadyKnife();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && !isMoving && !isBeingReady)
        {
            StartKnifeMovement();
        }

        if (isMoving)
        {
            MoveKnife();
        }
    }

    void ReadyKnife()
    {
        transform.position = Vector3.MoveTowards(transform.position, knifePoints[0].position, readySpeed * Time.deltaTime);
        if (Vector3.Distance(transform.position, knifePoints[0].position) < 0.001f)
        {
            isBeingReady = false;
            transform.position = knifePoints[0].position;
        }
    }

    void StartKnifeMovement()
    {
        isMoving = true;
        movingDown = true;
        startPosition = transform.position;
        targetPosition = startPosition + Vector3.down * 0.6f;
    }

    void MoveKnife()
    {
        if (movingDown)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                movingDown = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, startPosition, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, startPosition) < 0.001f)
            {
                isMoving = false;
                transform.position = knifePoints[knifeIndex].position;
            }
        }
    }
}