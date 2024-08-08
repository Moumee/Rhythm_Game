using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapPool : MonoBehaviour
{
    private List<GameObject> rightSlapPool = new List<GameObject>();
    private List<GameObject> leftSlapPool = new List<GameObject>(); 
    private int amountToPool = 5;

    [SerializeField] private GameObject rightSlapPrefab;
    [SerializeField] private GameObject leftSlapPrefab;

    private void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject rightObj = Instantiate(rightSlapPrefab, gameObject.transform);
            GameObject leftObj = Instantiate(leftSlapPrefab, gameObject.transform);   
            rightObj.SetActive(false);
            leftObj.SetActive(false);
            rightSlapPool.Add(rightObj);
            leftSlapPool.Add(leftObj);
        }
    }

    public GameObject GetRightSlap()
    {
        for (int i = 0; i < rightSlapPool.Count; i++)
        {
            if (!rightSlapPool[i].activeInHierarchy)
            {
                return rightSlapPool[i];
            }
        }
        return null;
    }
    public GameObject GetLeftSlap()
    {
        for (int i = 0; i < leftSlapPool.Count; i++)
        {
            if (!leftSlapPool[i].activeInHierarchy)
            {
                return leftSlapPool[i];
            }
        }
        return null;
    }

}
