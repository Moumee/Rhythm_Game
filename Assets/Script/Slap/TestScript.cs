using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestScript : MonoBehaviour
{
    [SerializeField] SlapPool slapObjectPool;
    [SerializeField] Transform leftSlapPosition;
    [SerializeField] Transform rightSlapPosition;
    [SerializeField] Animator fishAnim;
    [SerializeField] Animator waterAnim;
    [SerializeField] FishManager fishManager;
    [SerializeField] SlapManager slapManager;
    [SerializeField] Knife knife;
    public bool isSecondPart = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            slapManager.OnRightNoteHit();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            slapManager.OnLeftNoteHit();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && isSecondPart)
        {
            fishManager.OnNoteHit();
        }

    }
}
