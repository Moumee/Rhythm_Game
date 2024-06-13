using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Mold : MonoBehaviour
{
    [SerializeField] int positionId = 0;
    private bool isMoving = false;
    private int beatJumpCount;

    public PointSO pointData;
    Vector3[] standPoints;

    private float speed = 10f;
    public bool isLive = false;
    public bool isOnTime = false;
    Vector3 lastPos;

    // Start is called before the first frame update
    void Awake()
    {
        standPoints = pointData.moldPoints;
        isLive = true;
        beatJumpCount = 0;
        transform.position = standPoints[positionId];
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, standPoints[positionId], step);



    }



    public void Event_BeatCall()
    {
        beatJumpCount++;
        if (beatJumpCount > GameManager.Instance.beatJump - 1)
        {
            beatJumpCount = 0;
            SetNext();
        }

    }

    private void SetNext()
    {
        if (positionId == standPoints.Length - 1)
        {
            isLive = false;
            positionId = 0;
            transform.position = standPoints[positionId];
        }

        else if (positionId < standPoints.Length - 1)
        {
            ++positionId;
        }
    }

    

    

    IEnumerator WaitUntilTime()
    {
        yield return new WaitForSeconds(60 / GameManager.Instance.BPM * 1.5f);
        isOnTime = true;
        yield return new WaitForSeconds(60 / GameManager.Instance.BPM * 0.5f);
        isOnTime = false;
    }
}
