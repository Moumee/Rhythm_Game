using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filling : MonoBehaviour
{
    FillingManager fillingManager;
    int index;
    float elapsedTime = 0f;
    float fallDuration = 0.1f;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        fillingManager = FindObjectOfType<FillingManager>();
        index = FindObjectOfType<FillingManager>().callNumber;
        transform.position = fillingManager.fillingStartPos[index].position;
        startPos = fillingManager.fillingStartPos[index].position;

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
            int index = Random.Range(0, 3);
            switch (index)
            {
                case 0:
                    AudioManager.Instance.PlaySFX(AudioManager.SFX.Choco1);
                    break;
                case 1:
                    AudioManager.Instance.PlaySFX(AudioManager.SFX.Choco2);
                    break;
                case 2:
                    AudioManager.Instance.PlaySFX(AudioManager.SFX.Choco3);
                    break;
                default:
                    break;
            }
            if (fillingManager.callNumber != 2)
            {
                fillingManager.callNumber++;
            }
            else if (fillingManager.callNumber == 2)
            {
                fillingManager.callNumber = 0;
            }
            
            collision.GetComponent<SpriteRenderer>().enabled = true;
            Destroy(gameObject);
        }
    }
}
