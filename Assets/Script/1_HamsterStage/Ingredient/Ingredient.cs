using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class Ingredient : MonoBehaviour
{
    IngredientManager manager;
    public ObjectPool<Ingredient> _pool;
    public int positionId = 0;
    public Animator animator;
    [SerializeField] RuntimeAnimatorController[] controllers;
    private string currentState;
    private int beatJumpCount;
    private float moveDuration = 0.1f;

    [SerializeField] PointSO pointData;
    public Vector3[] standPoints;

    
    public bool isLive = false;
    public bool isOnTime = false;
    public bool cracked = false;

    private double catchableTime;

    private int serialnum;

    // Start is called before the first frame update
    void Awake()
    {
        manager = GetComponentInParent<IngredientManager>();
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = controllers[Random.Range(0, controllers.Length)];
        isLive = true;
        beatJumpCount = 0;
        //speed = 15/(60 / GameManager.Instance.BPM/3);
        //transform.position = standPoints[positionId].transform.position;
        standPoints = pointData.seedPoints;
    }
    private void OnEnable()
    {

        cracked = false;
        transform.position = standPoints[0];
        //catchableTime = AudioSettings.dspTime + 4 * (60 / GameManager.Instance.BPM);
        //serialnum = GameManager.Instance.noteNumber;


    }

    

    // Update is called once per frame
    void Update()
    {
        //if (!FindObjectOfType<PauseMenu>().isPlaying) { return;}

        
        if (transform.position.x == standPoints[1].x && !cracked)
        {
            animator.SetTrigger("Shake");
        }
        else if (transform.position.x == standPoints[2].x && !cracked)
        {
            animator.SetTrigger("Scared");

        }
        else if (transform.position.x < standPoints[2].x && !cracked)
        {
            animator.SetTrigger("Happy");
        }

        if (transform.position.x < standPoints[2].x + 2f && transform.position.x >= standPoints[2].x)
        {
            manager.targetIngre = this;
        }

        //if (serialnum == GameManager.Instance.judgeNumber) 
        //{
        //    isOnTime = true;
        //}
        //else
        //{
        //    isOnTime = false;
        //}

        //if (GameManager.Instance.count >= 151)
        //{
        //    _pool.Release(this);
        //}
        
    }

    
    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }

    //public void Event_BeatCall()
    //{
    //    beatJumpCount++;
    //    if(beatJumpCount > GameManager.Instance.noteBeatInterval-1) 
    //    {
            
    //        beatJumpCount = 0;
    //        SetNext();
    //    }
        
    //}

    public void MoveNext()
    {
        StartCoroutine(MoveCoroutine());
    }

    IEnumerator MoveCoroutine()
    {
        if (positionId < 4)
        {
            if (!cracked)
            {
                animator.SetTrigger("Move");
            }
            float timer = 0f;
            while (timer <= moveDuration)
            {
                timer += Time.deltaTime;
                transform.position = Vector3.Lerp(standPoints[positionId],
                    standPoints[positionId + 1], timer / moveDuration);
                yield return null;
            }
            transform.position = standPoints[positionId + 1];
            positionId++;
        }
        else
        {
            yield return new WaitForSeconds(moveDuration);  
            positionId = 0;
            animator.SetBool("Cracked", false);
            manager.activeIngredients.Remove(this);
            _pool.Release(this);
        }
    }

    //private void SetNext()
    //{
    //    if (positionId == standPoints.Length - 1)
    //    {
    //        isLive = false;
    //        positionId = 0;
    //        transform.position = standPoints[positionId].transform.position;
    //        _pool.Release(this);
    //    }

    //    else if (positionId < standPoints.Length - 1)
    //    {
    //        ++positionId;
    //        animator.SetTrigger("Move");
    //    }
    //}

    

    public void SetPool(ObjectPool<Ingredient> pool)
    {
        _pool = pool;
    }


    public void Break()
    {
        cracked = true;
        animator.SetBool("Cracked", true);
        animator.SetTrigger("Crack");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.crack);
    }


}
