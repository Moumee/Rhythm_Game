using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    [SerializeField] private float transitionDuration = 0.75f;
    private static SceneTransitionManager instance;
    private RawImage transitionOverlay;
    private readonly WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    private Canvas canvas;

    public static SceneTransitionManager Instance
    {
        get
        {
            if (!instance)
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
            SetupTransitionOverlay();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void SetupCanvas()
    {
        canvas = gameObject.AddComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 9999; // Ensure it renders on top of everything
        gameObject.AddComponent<CanvasScaler>();
        gameObject.AddComponent<GraphicRaycaster>();
    }

    private void SetupTransitionOverlay()
    {
        GameObject overlayGo = new GameObject("TransitionOverlay", typeof(RawImage));
        transitionOverlay = overlayGo.GetComponent<RawImage>();
        transitionOverlay.raycastTarget = false;
        transitionOverlay.transform.SetParent(transform, false);
        
        RectTransform rectTransform = transitionOverlay.rectTransform;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.sizeDelta = Vector2.zero;
        
        transitionOverlay.gameObject.SetActive(false);
    }

    public static void LoadSceneWithTransition(string sceneName)
    {
        Instance.StartCoroutine(Instance.TransitionCoroutine(sceneName));
    }
    
    private IEnumerator TransitionCoroutine(string sceneName)
{
    // Wait for end of frame to ensure clean screen capture
    yield return new WaitForEndOfFrame();

    // Capture current screen
    var width = Screen.width;
    var height = Screen.height;
    var rt = RenderTexture.GetTemporary(width, height, 24);
    var screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
    
    var originalRT = RenderTexture.active;
    var mainCamera = Camera.main;
    
    if (mainCamera != null)
    {
        mainCamera.targetTexture = rt;
        mainCamera.Render();
        RenderTexture.active = rt;
        
        screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenshot.Apply(false);
        
        mainCamera.targetTexture = null;
    }
    
    RenderTexture.active = originalRT;

    // Show the transition overlay with captured screenshot
    transitionOverlay.gameObject.SetActive(true);
    transitionOverlay.texture = screenshot;
    transitionOverlay.color = Color.white;

    // Start async scene load
    var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
    if (asyncOperation != null)
    {
        asyncOperation.allowSceneActivation = false;

        // Wait for the scene to load to 0.9 (90%)
        while (asyncOperation.progress < 0.9f)
        {
            yield return null;
        }

        // Activate the new scene
        asyncOperation.allowSceneActivation = true;

        // Wait one frame for the scene to actually change
        yield return null;

        // Perform fade out of the captured screenshot
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / transitionDuration;
            transitionOverlay.color = new Color(1f, 1f, 1f, 1f - t);
            yield return null;
        }
    }

    // Clean up
    RenderTexture.ReleaseTemporary(rt);
    Destroy(screenshot);
    transitionOverlay.texture = null;
    transitionOverlay.gameObject.SetActive(false);
}

    // private IEnumerator TransitionCoroutine(string sceneName)
    // {
    //     // Wait for end of frame to ensure clean screen capture
    //     yield return waitForEndOfFrame;
    //
    //     // Capture current screen
    //     var width = Screen.width;
    //     var height = Screen.height;
    //     var rt = RenderTexture.GetTemporary(width, height, 24);
    //     var screenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
    //     
    //     var originalRT = RenderTexture.active;
    //     var mainCamera = Camera.main;
    //     
    //     if (mainCamera != null)
    //     {
    //         mainCamera.targetTexture = rt;
    //         mainCamera.Render();
    //         RenderTexture.active = rt;
    //         
    //         screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
    //         screenshot.Apply(false);
    //         
    //         mainCamera.targetTexture = null;
    //     }
    //     
    //     RenderTexture.active = originalRT;
    //
    //     // Start async scene load
    //     var asyncOperation = SceneManager.LoadSceneAsync(sceneName);
    //     if (asyncOperation != null)
    //     {
    //         asyncOperation.allowSceneActivation = false;
    //
    //         // Show the transition overlay with captured screenshot
    //         transitionOverlay.gameObject.SetActive(true);
    //         transitionOverlay.texture = screenshot;
    //         transitionOverlay.color = Color.white;
    //
    //         // Wait for the scene to load to 0.9 (90%)
    //         while (asyncOperation.progress < 0.9f)
    //         {
    //             yield return null;
    //         }
    //
    //         // Activate the new scene
    //         asyncOperation.allowSceneActivation = true;
    //
    //         // Wait one frame for the scene to actually change
    //         yield return null;
    //
    //         // Capture new scene
    //         var newRt = RenderTexture.GetTemporary(width, height, 24);
    //         var newScreenshot = new Texture2D(width, height, TextureFormat.RGB24, false);
    //         
    //         mainCamera = Camera.main;
    //         if (mainCamera != null)
    //         {
    //             mainCamera.targetTexture = newRt;
    //             mainCamera.Render();
    //             RenderTexture.active = newRt;
    //             
    //             newScreenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
    //             newScreenshot.Apply(false);
    //             
    //             mainCamera.targetTexture = null;
    //         }
    //         
    //         RenderTexture.active = originalRT;
    //
    //         // Perform crossfade
    //         float elapsedTime = 0f;
    //         while (elapsedTime < transitionDuration)
    //         {
    //             elapsedTime += Time.deltaTime;
    //             float t = elapsedTime / transitionDuration;
    //             transitionOverlay.texture = screenshot;
    //             transitionOverlay.color = new Color(1f, 1f, 1f, 1f - t);
    //             yield return null;
    //
    //             // Draw new scene on top with increasing opacity
    //             Graphics.DrawTexture(new Rect(0, 0, width, height), newScreenshot, new Rect(0, 1, 1, -1), 0, 0, 0, 0, new Color(1f, 1f, 1f, t));
    //         }
    //     }
    //
    //     // Clean up
    //     RenderTexture.ReleaseTemporary(rt);
    //     Destroy(screenshot);
    //     transitionOverlay.texture = null;
    //     transitionOverlay.gameObject.SetActive(false);
    // }

    private void OnDestroy()
    {
        if (transitionOverlay != null)
        {
            Destroy(transitionOverlay.gameObject);
        }
    }
}