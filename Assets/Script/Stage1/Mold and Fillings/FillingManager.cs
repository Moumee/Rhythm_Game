using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillingManager : MonoBehaviour
{
    [SerializeField] GameObject fillingPrefab;
    public Transform[] fillingStartPos;
    public int callNumber;
    AudioManager.SFX[] chocoSounds = {AudioManager.SFX.Choco1, AudioManager.SFX.Choco2, AudioManager.SFX.Choco3};
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillingFall()
    {

        //AudioManager.Instance.PlaySFX(chocoSounds[Random.Range(0, chocoSounds.Length)]);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.chocolate);


        switch (callNumber)
        {
            case 0:
                Instantiate(fillingPrefab);
                break;
            case 1:
                Instantiate(fillingPrefab);
                break;
            case 2:
                Instantiate(fillingPrefab);
                break;
            default:
                break;
        }
    }

    public void Event_FillMiss()
    {
        Debug.Log("d");
        if (callNumber != 2)
        {
            callNumber++;
        }
        else if (callNumber == 2)
        {
            callNumber = 0;
        }
    }
}
