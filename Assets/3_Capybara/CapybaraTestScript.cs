using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapybaraTestScript : MonoBehaviour
{
    public TangerineManager tangerineManager;
    public TangerineFallManger tangerineFallManger;
    public TangerineCandyManager tangerineCandyManager;
    public GameObject firstSubStage;
    public GameObject secondSubStage;
    public GameObject thirdSubStage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (firstSubStage.gameObject.activeInHierarchy)
            {
                tangerineManager.OnNoteHit();
            }
            else if (secondSubStage.gameObject.activeInHierarchy)
            {
                tangerineFallManger.OnNoteHit();
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (secondSubStage.gameObject.activeInHierarchy)
            {
                tangerineFallManger.OnNoteMiss();
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            tangerineCandyManager.OnRightNoteHit();
        }
        if (Input.GetKeyDown (KeyCode.LeftArrow))
        {
            tangerineCandyManager.OnLeftNoteHit();
        }
    }
}
