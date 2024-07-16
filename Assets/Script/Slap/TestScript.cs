using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    [SerializeField] SlapPool slapObjectPool;
    [SerializeField] Transform leftSlapPosition;
    [SerializeField] Transform rightSlapPosition;
    [SerializeField] Animator fishAnim;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            GameObject leftSlap = slapObjectPool.GetLeftSlap();
            leftSlap.transform.position = leftSlapPosition.position;
            leftSlap.SetActive(true);
            fishAnim.SetTrigger("LeftHit");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            GameObject rightSlap = slapObjectPool.GetRightSlap();
            rightSlap.transform.position = rightSlapPosition.position;
            rightSlap.SetActive(true);
            fishAnim.SetTrigger("RightHit");
        }
    }
}
