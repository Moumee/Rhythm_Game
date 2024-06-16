using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class Ingredient : MonoBehaviour
{
    public ObjectPool<Ingredient> _pool;
    [SerializeField] int positionId = 0;
    public Animator animator;
    [SerializeField] RuntimeAnimatorController[] contollers;
    private string currentState;
    private int beatJumpCount;

    [SerializeField] GameObject[] standPoints;

    
    private float speed = 60f;
    public bool isLive = false;
    public bool isOnTime = false;
    public bool cracked = false;

    private float catchableTime;

    private int serialnum;

    // Start is called before the first frame update
    void Awake()
    {
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = contollers[Random.Range(0, contollers.Length)];
        isLive = true;
        beatJumpCount = 0;
        //speed = 15/(60 / GameManager.Instance.BPM/3);
        //transform.position = standPoints[positionId].transform.position;
        
    }
    private void OnEnable()
    {
        catchableTime = Time.time + 4 * (60 / GameManager.Instance.BPM);
        serialnum = GameManager.Instance.noteNumber;
        cracked = false;
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, standPoints[positionId].transform.position, step);

        if (transform.position.x < standPoints[0].transform.position.x && transform.position.x > standPoints[1].transform.position.x)
        {
            animator.SetTrigger("Move");
        }
        else if (transform.position.x == standPoints[1].transform.position.x && !cracked)
        {
            animator.SetTrigger("Shake");
        }
        else if (transform.position.x == standPoints[2].transform.position.x && !cracked)
        {
            animator.SetTrigger("Scared");
            
        }
        else if (transform.position.x < standPoints[2].transform.position.x && !cracked)
        {
            animator.SetTrigger("Happy");
        }

        if (serialnum == GameManager.Instance.judgeNumber) 
        {
            isOnTime = true;
        }
        else
        {
            isOnTime = false;
        }

        if (GameManager.Instance.count >= 151)
        {
            _pool.Release(this);
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
        cracked = true;
        animator.SetTrigger("Crack");
        int index = Random.Range(0, 3);
        switch (index)
        {
            case 0:
                AudioManager.Instance.PlaySFX(AudioManager.SFX.Crack1);
                break;
            case 1:
                AudioManager.Instance.PlaySFX(AudioManager.SFX.Crack2);
                break;
            case 2:
                AudioManager.Instance.PlaySFX(AudioManager.SFX.Crack3);
                break;
            default:
                break;
        }
        gameObject.transform.position += Vector3.down;
    }


}
