using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ReplyController : MonoBehaviour
{
    public Sprite[] commentsList = new Sprite[0];
    private GameObject[] replies = new GameObject[5];

    public GameObject replyPrefeb;

    // Start is called before the first frame update
    void Start()
    {
        bool isSuccess;
        for(int i = 0; i < 5; i++) 
        {
            replies[i] = Instantiate(replyPrefeb, GameObject.Find("ReplyContainer").transform);
            isSuccess = ScoreStorage.Instance.isSuccess[i];
            // commentsList에서 원하는 스프라이트 선택
            replies[i].GetComponent<Reply>().initialize(commentsList[2*i + (isSuccess ? 0 : 1)], i);
            
        }

        //AllApear(); //테스트용
    }

    // Update is called once per frame


    public void AllSlide()
    {
        foreach (var rp in replies)
        {
            rp.GetComponent<Reply>().Slide();
        }
    }

    public void AllApear()
    {
        StartCoroutine(AllApearCoruotine());
    }

    IEnumerator AllApearCoruotine()
    {
        foreach (var rp in replies)
        {
            yield return new WaitForSeconds(0.5f);
            rp.GetComponent<Reply>().Apear();
        }
        yield return null;
    }
}
