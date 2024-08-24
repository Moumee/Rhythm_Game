using System.Collections;
using UnityEngine;

public class Tomato : MonoBehaviour
{
    public TomatoManager manager;
    Animator animator;
    Animator sliceAnim;
    GameObject pivotObj;
    SpriteRenderer spriteRenderer;
    public bool isMoving = false;
    public GameObject background;
    private Vector3 randomPos;
    private Vector3 startPos = Vector3.zero;
    private float shakeDuration = 0.15f;
    private float shakeDistance = 0.2f;
    private float delayBetweenShakes = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sliceAnim = transform.GetChild(0).GetComponent<Animator>();
        pivotObj = transform.GetChild(1).gameObject;
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnNoteHit()
    {
        if (isMoving)
        {
            sliceAnim.gameObject.transform.position = pivotObj.transform.position;
            sliceAnim.SetTrigger("Slice");
            spriteRenderer.enabled = false;
            isMoving = false;
            manager.movingTomatoes.Remove(this);
            manager.availableTomatoes.Add(this);
            StartCoroutine(ShakeCoroutine());
        }
        
    }

    public void MoveStateTrue()
    {
        spriteRenderer.enabled = true;
        isMoving = true;
        manager.availableTomatoes.Remove(this);
        manager.movingTomatoes.Add(this);
    }

    public void MoveStateFalse()
    {
        isMoving = false;
        if (!manager.availableTomatoes.Contains(this))
        {
            manager.availableTomatoes.Add(this);
        }
        manager.movingTomatoes.Remove(this);
    }

    public void Appear()
    {
        animator.SetTrigger("Appear");
    }

    private IEnumerator ShakeCoroutine()
    {
        float timer = 0f;
        while (timer < shakeDuration)
        {
            randomPos = startPos + (Random.insideUnitSphere * shakeDistance);
            background.transform.position = randomPos;
            yield return new WaitForSeconds(delayBetweenShakes);
            timer += delayBetweenShakes;
        }

        background.transform.position = startPos;
    }
}
