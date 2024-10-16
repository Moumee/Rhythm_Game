using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filling : MonoBehaviour
{
    FillingManager fillingManager;
    int index;
    float elapsedTime = 0f;
    float fallDuration = 0.1f;
    public Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        fillingManager = FindObjectOfType<FillingManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedTime <= fallDuration)
        {
            elapsedTime += Time.deltaTime;
        }
        transform.position = Vector3.Lerp(startPos, startPos + new Vector3(0, -10f, 0), elapsedTime / fallDuration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Hole"))
        {
            
            collision.GetComponent<SpriteRenderer>().enabled = true;
            Destroy(gameObject);
        }
    }
}
