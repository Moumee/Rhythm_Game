using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public float sceneFadeDuration;

    SceneFade sceneFade;

    private void Awake()
    {
        sceneFade = GetComponentInChildren<SceneFade>();
        StartCoroutine(sceneFade.FadeInCoroutine(sceneFadeDuration));
    }


    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));  
    }
    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        yield return sceneFade.FadeOutCoroutine(sceneFadeDuration);
        yield return SceneManager.LoadSceneAsync(sceneName);

    }
}
