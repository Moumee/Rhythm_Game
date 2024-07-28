using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevisedNotePool : MonoBehaviour
{
    private List<GameObject> rightNotePool = new List<GameObject>();
    private List<GameObject> leftNotePool = new List<GameObject>();
    private List<GameObject> upNotePool = new List<GameObject>();
    private List<GameObject> downNotePool = new List<GameObject>(); 
    private int amountToPool = 4;

    [SerializeField] private GameObject rightNotePrefab;
    [SerializeField] private GameObject leftNotePrefab;
    [SerializeField] private GameObject upNotePrefab;
    [SerializeField] private GameObject downNotePrefab;

    private void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject rightNote = Instantiate(rightNotePrefab, gameObject.transform);
            GameObject leftNote = Instantiate(leftNotePrefab, gameObject.transform);
            GameObject upNote = Instantiate(upNotePrefab, gameObject.transform);
            GameObject downNote = Instantiate(downNotePrefab, gameObject.transform);
            rightNote.SetActive(false);
            leftNote.SetActive(false);
            upNote.SetActive(false);
            downNote.SetActive(false);
            rightNotePool.Add(rightNote);
            leftNotePool.Add(leftNote);
            upNotePool.Add(upNote);
            downNotePool.Add(downNote);
        }
    }

    public GameObject GetRightNote()
    {
        for (int i = 0; i < rightNotePool.Count; i++)
        {
            if (!rightNotePool[i].activeInHierarchy)
            {
                return rightNotePool[i];
            }
        }
        return null;
    }
    public GameObject GetLeftNote()
    {
        for (int i = 0; i < leftNotePool.Count; i++)
        {
            if (!leftNotePool[i].activeInHierarchy)
            {
                return leftNotePool[i];
            }
        }
        return null;
    }

    public GameObject GetUpNote()
    {
        for (int i = 0; i < upNotePool.Count; i++)
        {
            if (!leftNotePool[i].activeInHierarchy)
            {
                return upNotePool[i];
            }
        }
        return null;
    }

    public GameObject GetDownNote()
    {
        for (int i = 0; i < downNotePool.Count; i++)
        {
            if (!downNotePool[i].activeInHierarchy)
            {
                return downNotePool[i];
            }
        }
        return null;
    }
}
