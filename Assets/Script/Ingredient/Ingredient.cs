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
    private bool isMoving = false;

    [SerializeField] GameObject[] standPoints;

    private float speed = 10f;
    public bool isLive = false;
    public bool isOnTime = false;
    Vector3 lastPos;
    
    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        isLive = true;
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

        
    }

    
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }

    public void SetNext()
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

    public void SetOnTime()
    {

    }

    IEnumerator WaitUntilTime()
    {
        yield return new WaitForSeconds(60 / GameManager.Instance.BPM * 1.5f);
        isOnTime = true;
        yield return new WaitForSeconds(60 / GameManager.Instance.BPM * 0.5f);
        isOnTime = false;
    }
}
