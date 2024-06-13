using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GridBrushBase;
using UnityEngine.Pool;
using TreeEditor;

public class Ingredient : MonoBehaviour
{
    private ObjectPool<Ingredient> _pool;
    [SerializeField] int positionId = 0;
    Animator animator;
    private string currentState;
    private int beatJumpCount;

    [SerializeField] GameObject[] standPoints;

    private float speed = 60f;
    public bool isLive = false;
    public bool isOnTime = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
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

        if (Mathf.Abs(standPoints[2].transform.position.x -transform.position.x)<=1.5f ) 
        {
            isOnTime = true;
        }
        else
        {
            isOnTime = false;
        }
        
    }

    
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }

    public void Event_BeatCall()
    {
        beatJumpCount++;
        if(beatJumpCount > GameManager.Instance.beatJump-1) 
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
            animator.SetTrigger("Move");
        }
    }

    public void SetPoint(GameObject[] standPoint)
    {
        this.standPoints = standPoint;
        isLive = true;
    }

    public void SetPool(ObjectPool<Ingredient> pool)
    {
        _pool = pool;
    }


    public void Break()
    {
        animator.SetTrigger("Break");
        gameObject.transform.position += Vector3.down;
    }
}
