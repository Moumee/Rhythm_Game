using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillingManager : MonoBehaviour
{
    [SerializeField] GameObject fillingPrefab;
    public Transform[] fillingStartPos;
    AudioManager.SFX[] chocoSounds = {AudioManager.SFX.Choco1, AudioManager.SFX.Choco2, AudioManager.SFX.Choco3};
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FillingFall(int callNumber)
    {

        //AudioManager.Instance.PlaySFX(chocoSounds[Random.Range(0, chocoSounds.Length)]);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.chocolate);

        GameObject fallinFilling = Instantiate(fillingPrefab);
        fallinFilling.GetComponent<Filling>().startPos = fillingStartPos[callNumber - 1].position;
        
    }

   
}
