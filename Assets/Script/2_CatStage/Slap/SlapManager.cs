using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapManager : MonoBehaviour
{
    SlapPool pool;
    [SerializeField] Animator fishAnim;
    [SerializeField] Animator waterAnim;
    [SerializeField] Transform leftSlapPosition;
    [SerializeField] Transform rightSlapPosition;
    // Start is called before the first frame update
    void Awake()
    {
        pool = GetComponent<SlapPool>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnLeftNoteHit()
    {
        GameObject leftSlap = pool.GetLeftSlap();
        leftSlap.transform.position = leftSlapPosition.position;
        leftSlap.SetActive(true);
        fishAnim.SetTrigger("LeftHit");
        waterAnim.SetTrigger("WaterLeft");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.slap);
    }

    public void OnRightNoteHit()
    {
        GameObject rightSlap = pool.GetRightSlap();
        rightSlap.transform.position = rightSlapPosition.position;
        rightSlap.SetActive(true);
        fishAnim.SetTrigger("RightHit");
        waterAnim.SetTrigger("WaterRight");
        AudioManager.Instance.PlaySFX(AudioManager.Instance.slap);
    }

    public void OnNoteMiss()
    {
        //Do nothing
    }
}
