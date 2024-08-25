using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public GameObject anyKeyObj;
    public Animator fadeAnim;
    public string nextSceneName;
    
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.bell);
        AudioManager.Instance.PlayBGM(AudioManager.Instance.restaurant);
    }

    // Update is called once per frame
    void Update()
    {
        if (anyKeyObj.activeInHierarchy)
        {
            if (Input.anyKeyDown)
            {
                if (!Input.GetKeyDown(KeyCode.Escape) && !Input.GetKeyDown(KeyCode.Mouse0) && !Input.GetKeyDown(KeyCode.Mouse1))
                {
                    anyKeyObj.SetActive(false);
                    fadeAnim.SetTrigger("FadeOut");
                    StartCoroutine(LoadNextScene(nextSceneName));
                }
            }
        }
    }

    public void SetAnyKeyActive()
    {
        anyKeyObj.SetActive(true);  
    }

    IEnumerator LoadNextScene(string sceneName)
    {
        AudioManager.Instance.StopAllMusic();
        yield return new WaitForSeconds(fadeAnim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        SceneManager.LoadSceneAsync(sceneName);
    }
}
