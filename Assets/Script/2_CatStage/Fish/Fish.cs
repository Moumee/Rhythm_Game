using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public class Fish : MonoBehaviour
{
    public PointSO pointData;
    public int positionId = 0;
    private FishManager fishManager;
    public GameObject[] cutObjects;
    public bool isMoving = false;
    private float moveDuration = 0.05f;

    
    private void OnEnable()
    {
        fishManager = FindObjectOfType<FishManager>();
        cutObjects = new GameObject[transform.childCount];

        for (int i = 0; i < cutObjects.Length; i++)
        {
            cutObjects[i] = transform.GetChild(i).gameObject;
        }
    }


    // Update is called once per frame
    void Update()
    {
        //If position is at spawn point, disable fish cuts.
        if (positionId == 0)
        {
            foreach (var cutObject in cutObjects)
            {
                cutObject.SetActive(false);
            }
        }


        
    }

    public void MoveFish()
    {
        StartCoroutine(MoveFishLeftCoroutine());
    }

    private IEnumerator MoveFishLeftCoroutine()
    {
        isMoving = true;
        int nextIndex = (positionId + 1) % pointData.fishWaypoints.Length;
        float elapsedTime = 0;
        while (moveDuration >= elapsedTime)
        {
            transform.position = Vector3.Lerp(pointData.fishWaypoints[positionId],
                pointData.fishWaypoints[nextIndex], elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        positionId = nextIndex;
        transform.position = pointData.fishWaypoints[nextIndex];
        if (positionId == pointData.fishWaypoints.Length - 1)
        {
            transform.position = pointData.fishWaypoints[0];
            positionId = 0;
        }
        isMoving = false;

    }


}
