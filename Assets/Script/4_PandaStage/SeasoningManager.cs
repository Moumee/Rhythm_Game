using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeasoningManager : MonoBehaviour
{
    public Transform[] wayPoints;
    public Dictionary<string, GameObject> seasonings = new Dictionary<string, GameObject>();
    public SoySauce soySauce;
    public SugarSpoon sugarSpoon;
    public Vinegar vinegar;
    public GameObject currentObject;
    
    // Start is called before the first frame update
    void Awake()
    {
        seasonings["Soy"] = soySauce.gameObject;
        seasonings["Sugar"] = sugarSpoon.gameObject;
        seasonings["Vinegar"] = vinegar.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentObject == null)
        {
            foreach (var seasoning in seasonings)
            {
                if (seasoning.Value.transform.position == wayPoints[1].position)
                {
                    currentObject = seasoning.Value;
                    break;
                }
            }
        }

    }

    public void OnNoteHit()
    {
        
    }

    public void OnNoteMiss()
    {

    }

    
}
