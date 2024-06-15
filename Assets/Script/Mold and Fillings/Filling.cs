using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filling : MonoBehaviour
{
    [SerializeField] PointSO pointData;
    int index;
    float elapsedTime = 0f;
    float fallDuration = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        index = FindObjectOfType<FillingManager>().callNumber;
        transform.position = pointData.fillingSpawnPoints[index];
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedTime <= fallDuration)
        {
            elapsedTime += Time.deltaTime;
        }
        if (transform.position == pointData.fillingLandPoints[index]) Destroy(this.gameObject);
        transform.position = Vector3.Lerp(pointData.fillingSpawnPoints[index], pointData.fillingLandPoints[index], elapsedTime / fallDuration);
    }
}
