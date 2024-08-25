using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalResultButton : MonoBehaviour
{
    private float buttonScale = 1.2f;
    Vector2 defaultScale = new Vector2 (1, 1);
    public EventReference appearSound;
    public float waitTime = 3f;
    private float appearTime = 0.2f;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(Appear());
    }
    public void PointerEnter()
    {
        transform.localScale = defaultScale * buttonScale;
    }

    public void PointerExit()
    {
        transform.localScale = defaultScale;
    }

    public void PlayClick()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.click);
    }

    public void PlayAppearSFX()
    {
        AudioManager.Instance.PlaySFX(appearSound);
    }

    public void Restart()
    {
        AudioManager.Instance.StopAllMusic();
        SceneManager.LoadSceneAsync("1-1");
    }

    public void Home()
    {
        AudioManager.Instance.StopAllMusic();
        SceneManager.LoadSceneAsync("StartMenu");
    }

    IEnumerator Appear()
    {
        yield return new WaitForSeconds(waitTime);
        AudioManager.Instance.PlaySFX(appearSound);
        float timer = 0f;
        while (timer < appearTime)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, timer / appearTime);
            yield return null;
        }
        transform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
