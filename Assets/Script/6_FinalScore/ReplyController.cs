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
            replies[i].transform.rotation = Quaternion.identity;
            isSuccess = ScoreStorage.Instance.isSuccess[i];
            // commentsList���� ���ϴ� ��������Ʈ ����
            replies[i].GetComponent<Reply>().initialize(commentsList[2*i + (isSuccess ? 0 : 1)], i);
            
        }

        //AllApear(); //�׽�Ʈ��
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
        yield return new WaitForSeconds(0.5f);
        foreach (var rp in replies)
        {
            
            rp.GetComponent<Reply>().Apear();
            yield return new WaitForSeconds(0.4f);
        }
        //AllSlide();
       // yield return new WaitForSeconds(2f);
        //AllSlide();

        yield return null;
    }
}
