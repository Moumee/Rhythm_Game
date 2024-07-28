using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;

public class Fish : MonoBehaviour
{
    public PointSO pointData;
    private float speed = 100f;
    public int positionId = 0;
    private FishManager fishManager;
    public GameObject[] cutObjects;

    
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


        float step = speed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, pointData.fishWaypoints[positionId], step);
    }

    public void MoveFish()
    {   
        if (positionId < pointData.fishWaypoints.Length - 1)
        {
            positionId++;
        }
        else if (positionId == pointData.fishWaypoints.Length - 1)
        {
            gameObject.transform.position = pointData.fishWaypoints[0];
            positionId = 0;
        }
    }


}
