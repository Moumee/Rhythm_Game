using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GridBrushBase;
using UnityEngine.Pool;

public class Ingredient : MonoBehaviour
{
    private ObjectPool<Ingredient> _pool;
    [SerializeField] int positionId = 0;

    [SerializeField] GameObject[] standPoints;

    public float speed;
    
    // Start is called before the first frame update
    void Awake()
    {
        //transform.position = standPoints[positionId].transform.position;
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, standPoints[positionId].transform.position, speed);
    }

    public void SetNext()
    {
        if (positionId > standPoints.Length - 1)
        {
            _pool.Release(this);
        }

        if (positionId < standPoints.Length - 1) 
        { 
            ++positionId; 
        }
    }

    public void SetPoint(GameObject[] standPoint)
    {
        this.standPoints = standPoint;
    }

    public void SetPool(ObjectPool<Ingredient> pool)
    {
        _pool = pool;
    }

}
