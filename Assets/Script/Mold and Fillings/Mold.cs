using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GridBrushBase;
using UnityEngine.Pool;
using TreeEditor;

public class Mold : MonoBehaviour
{
    private ObjectPool<Mold> _pool;
    [SerializeField] int positionId = 0;
    private string currentState;
    private int beatJumpCount;

    [SerializeField] GameObject[] standPoints;

    private float speed = 60f;
    public bool isLive = false;
    public bool isOnTime = false;

    // Start is called before the first frame update
    void Awake()
    {
        isLive = true;
        beatJumpCount = 0;
        //transform.position = standPoints[positionId].transform.position;
    }

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, standPoints[positionId].transform.position, step);

        if (Mathf.Abs(standPoints[1].transform.position.x - transform.position.x) <= 1.5f)
        {
            isOnTime = true;
        }
        else
        {
            isOnTime = false;
        }

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
            transform.position = standPoints[positionId].transform.position;
            _pool.Release(this);
        }

        else if (positionId < standPoints.Length - 1)
        {
            ++positionId;
        }
    }

    public void SetPoint(GameObject[] standPoint)
    {
        this.standPoints = standPoint;
        isLive = true;
    }

    public void SetPool(ObjectPool<Mold> pool)
    {
        _pool = pool;
    }


    
}
