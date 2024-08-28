using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TangerineCandy : MonoBehaviour
{
    public Sprite centerSprite;
    public Sprite[] rightSides;
    public Sprite[] leftSides;
    private SpriteRenderer spriteRenderer;
    public TangerineCandyManager manager;
    private float moveDuration = 0.1f;
    private int index = 0;
    public int pointIndex;
    private float moveDelay = 0.3f;
    
    // Start is called before the first frame update
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnRight()
    {
        index++;
        if (index < rightSides.Length)
        {
            spriteRenderer.sprite = rightSides[index];
        }
    }

    public void TurnLeft()
    {
        index++;
        if (index < leftSides.Length)
        {
            spriteRenderer.sprite = leftSides[index];
        }
        
    }

    public void MoveLeft()
    {
        StartCoroutine(MoveLeftCoroutine());
    }

    private IEnumerator MoveLeftCoroutine()
    {
        manager.isMoving = true;
        yield return new WaitForSeconds(moveDelay);
        int nextIndex = (pointIndex + 1) % 3;
        float elapsedTime = 0;
        while (moveDuration >= elapsedTime)
        {
            transform.position = Vector3.Lerp(manager.candyPoints[pointIndex].position,
                manager.candyPoints[nextIndex].position, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        pointIndex = nextIndex;
        transform.position = manager.candyPoints[nextIndex].position;
        if (pointIndex == 2)
        {
            transform.position = manager.candyPoints[0].position;
            index = 0;
            pointIndex = 0;
            spriteRenderer.sprite = centerSprite;
        }
        manager.isMoving = false;
    }
}
