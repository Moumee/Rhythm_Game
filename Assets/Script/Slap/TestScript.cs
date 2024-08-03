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
    [SerializeField] Knife knife;
    public bool isSecondPart = false;
    private bool ff=false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.LeftArrow))
        //{
        //    if (!isSecondPart)
        //    {
        //        GameObject leftSlap = slapObjectPool.GetLeftSlap();
        //        leftSlap.transform.position = leftSlapPosition.position;
        //        leftSlap.SetActive(true);
        //        fishAnim.SetTrigger("LeftHit");
        //        waterAnim.SetTrigger("WaterLeft");
        //    }
        //}
        //else if (Input.GetKeyDown(KeyCode.RightArrow))
        //{
        //    if (!isSecondPart)
        //    {
        //        GameObject rightSlap = slapObjectPool.GetRightSlap();
        //        rightSlap.transform.position = rightSlapPosition.position;
        //        rightSlap.SetActive(true);
        //        fishAnim.SetTrigger("RightHit");
        //        waterAnim.SetTrigger("WaterRight");
        //    }
        //}

        //if (Input.GetKeyDown(KeyCode.DownArrow) && isSecondPart)
        //{
        //    fishManager.currentFish.cutObjects[knife.knifeIndex].SetActive(true);
        //    fishManager.VibrateCurrentFish();
        //}

    }

    public void Slap()
    {
        if (ff)
        {
            GameObject leftSlap = slapObjectPool.GetLeftSlap();
            leftSlap.transform.position = leftSlapPosition.position;
            leftSlap.SetActive(true);
            fishAnim.SetTrigger("LeftHit");
            waterAnim.SetTrigger("WaterLeft");
            ff = false;
        }
        else
        {
            GameObject rightSlap = slapObjectPool.GetRightSlap();
            rightSlap.transform.position = rightSlapPosition.position;
            rightSlap.SetActive(true);
            fishAnim.SetTrigger("RightHit");
            waterAnim.SetTrigger("WaterRight");
            ff = true;
        }
    }

    public void Chop()
    {
        fishManager.currentFish.cutObjects[knife.knifeIndex].SetActive(true);
        fishManager.VibrateCurrentFish();
    }
}
