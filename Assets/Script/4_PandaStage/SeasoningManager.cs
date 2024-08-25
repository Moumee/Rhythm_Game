using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeasoningManager : MonoBehaviour
{
    public EventReference pourSauceSFX;
    public Transform[] wayPoints;
    public List<Seasoning> seasonings = new List<Seasoning>();
    public List<Seasoning> readySeasonings = new List<Seasoning>();
    public Seasoning currentSeasoning;
    
    // Start is called before the first frame update
    void Awake()
    {
        Seasoning.onMove += MoveReadySeasonings;
    }

    // Update is called once per frame
    void Update()
    {

        foreach (var seasoning in seasonings)
        {
            if (seasoning.transform.position.x >= 0 && seasoning.transform.position.x < 10)
            {
                currentSeasoning = seasoning;
                readySeasonings.Remove(seasoning);
                break;
            }
        }
        
        

    }

    private void OnDestroy()
    {
        Seasoning.onMove -= MoveReadySeasonings;
    }

    public void MoveReadySeasonings()
    {
        readySeasonings[Random.Range(0, readySeasonings.Count)].MoveNext();
    }

    public void OnNoteHit()
    {
        AudioManager.Instance.PlaySFX(pourSauceSFX);    
        currentSeasoning.OnNoteHit();
        currentSeasoning.MoveNextCheck();
    }

    public void OnNoteMiss()
    {
        currentSeasoning.OnNoteMiss();
        currentSeasoning.MoveNextCheck();   
    }

    
}
