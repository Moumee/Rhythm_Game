using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public float transitionDuration = 1f;
    private static SceneTransitionManager instance;

    public static SceneTransitionManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("SceneTransitionManager");
                instance = go.AddComponent<SceneTransitionManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SetupCanvas();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void SetupCanvas()
    {
        Canvas canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 9999; // Ensure it renders on top of everything
        gameObject.AddComponent<CanvasScaler>();
        gameObject.AddComponent<GraphicRaycaster>();
    }

    public static void LoadSceneWithTransition(string sceneName)
    {
        Instance.StartCoroutine(Instance.TransitionCoroutine(sceneName));
    }

    private IEnumerator TransitionCoroutine(string sceneName)
    {
        // Capture the current scene
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 24);
        Camera.main.targetTexture = rt;
        Camera.main.Render();
        Camera.main.targetTexture = null;

        Texture2D screenshot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        RenderTexture.active = rt;
        screenshot.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        screenshot.Apply();

        RenderTexture.active = null;
        Destroy(rt);

        // Load the new scene
        SceneManager.LoadScene(sceneName);

        // Wait for the new scene to load
        yield return null;

        // Create a RawImage to display the screenshot
        RawImage rawImage = new GameObject("TransitionOverlay").AddComponent<RawImage>();
        rawImage.texture = screenshot;
        rawImage.raycastTarget = false; // This makes the RawImage not block raycasts

        // Set the RawImage as a child of our canvas
        rawImage.transform.SetParent(transform, false);

        // Ensure the RawImage covers the entire screen
        RectTransform rectTransform = rawImage.rectTransform;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.sizeDelta = Vector2.zero;

        // Fade out the screenshot
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1f - (elapsedTime / transitionDuration);
            rawImage.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }

        // Clean up
        Destroy(rawImage.gameObject);
        Destroy(screenshot);
    }
}