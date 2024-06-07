using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UnityEvent CatchBeat;
    public static GameManager Instance;
   
    public GameObject[] notePoints;

    public GameObject noteSyncPoint;

    public float BPM = 105;
    private float interval;
    private float margin = 0.114f;
    private float inputTime;
    private float timer;
    public int count = 0;

    void Awake()
    {
        SceneManager.sceneLoaded += PlaySceneBGM;
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
        }

        interval = 60 / BPM;
        timer = Time.time;
    }

    void PlaySceneBGM(Scene scene, LoadSceneMode mode)
    {
        switch (scene.buildIndex)
        {
            case 1:

                break;
            default:
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {

        if (Time.time - timer >= BPM/60)
        {
            ++count;
            timer = Time.time;
            CatchBeat.Invoke();
        }

        if (Input.GetKeyUp(KeyCode.Space)) 
        { 
            inputTime = Time.time;
        }
    }
}
