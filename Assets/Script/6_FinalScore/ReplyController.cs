using System.Collections;
using UnityEngine;

public class ReplyController : MonoBehaviour
{
    public Sprite[] commentsList = new Sprite[0];
    private GameObject[] replies = new GameObject[5];

    public GameObject replyPrefeb;


    public void AllSlide()
    {
        foreach (var rp in replies)
        {
            rp.GetComponent<Reply>().Slide();
        }
    }

    public void AllAppear()
    {
        for (int i = 0; i < 5; i++)
        {
            replies[i] = Instantiate(replyPrefeb, GameObject.Find("ReplyContainer").transform);
            replies[i].transform.rotation = Quaternion.identity;
            bool isSuccess = ScoreStorage.Instance.isSuccess[i];
            // commentsList���� ���ϴ� ��������Ʈ ����
            replies[i].GetComponent<Reply>().Initialize(commentsList[2 * i + (isSuccess ? 0 : 1)], i);

        }
        StartCoroutine(AllAppearCoruotine());
    }

    IEnumerator AllAppearCoruotine()
    {
        yield return new WaitForSeconds(0.5f);
        foreach (var rp in replies)
        {
            
            rp.GetComponent<Reply>().Appear();
            yield return new WaitForSeconds(0.4f);
        }
        //AllSlide();
       // yield return new WaitForSeconds(2f);
        //AllSlide();

        yield return null;
    }
}
