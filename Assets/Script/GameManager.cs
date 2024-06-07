using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public UnityEvent CatchBeat;
    public UnityEvent CatchNote;
    public static GameManager Instance;

    private List<int> testChart = new List<int> { 1, 1, 0, 0, 1, 0, 1, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 };
   
    public GameObject[] notePoints;

    public GameObject noteSyncPoint;

    public float BPM = 105;
    private float interval;
    private float margin = 0.114f;
    private float inputError;
    private float timer;
    private bool socoreChance = false;

    public int count = 0;


    public int Score = 0;

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

        if (Time.time - timer >= interval)
        {
            if (testChart[count] == 1) 
            {
                CatchBeat.Invoke();
            }
            ++count;
            timer = Time.time;
            if (testChart[count - 1] == 0) { 
            }
            
        }

        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            inputError = Time.time - timer;

            if (inputError < interval/2)
            {
                if(inputError <= margin && testChart[count-1] == 1)
                {
                    ++Score;
                }
            }
            else
            {
                if (inputError > interval - margin && testChart[count] == 1)
                {
                    ++Score;
                }
            }
        }
    }
}
