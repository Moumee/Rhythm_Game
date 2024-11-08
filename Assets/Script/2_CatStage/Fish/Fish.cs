using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fish : MonoBehaviour
{
    public PointSO pointData;
    public int positionId = 0;
    private FishManager fishManager;
    public GameObject[] cutObjects;
    public bool isMoving = false;
    private float moveDuration = 0.1f;

    
    private void Awake()
    {
        fishManager = FindObjectOfType<FishManager>();
        cutObjects = new GameObject[transform.childCount];

        for (int i = 0; i < cutObjects.Length; i++)
        {
            cutObjects[i] = transform.GetChild(i).gameObject;
        }
        
        if (positionId == 1)
        {
            fishManager.currentFish = this;
        }
    }
    

    public void MoveFish()
    {
        StartCoroutine(MoveFishLeftCoroutine());
    }

    private IEnumerator MoveFishLeftCoroutine()
    {
        isMoving = true;
        int nextIndex = positionId + 1;
        if (nextIndex < pointData.fishWaypoints.Length)
        {
            float elapsedTime = 0;
            while (moveDuration >= elapsedTime)
            {
                transform.position = Vector3.Lerp(pointData.fishWaypoints[positionId],
                    pointData.fishWaypoints[nextIndex], elapsedTime / moveDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }
            positionId = nextIndex;
            transform.position = pointData.fishWaypoints[positionId];
            if (positionId == 1)
            {
                fishManager.currentFish = this;
            }
        }
        
        else if (nextIndex == pointData.fishWaypoints.Length)
        {
            yield return new WaitForSeconds(moveDuration);
            transform.position = pointData.fishWaypoints[0];
            positionId = 0;
            foreach (var cutObject in cutObjects)
            {
                cutObject.SetActive(false);
            }
        }
        isMoving = false;

    }


}
