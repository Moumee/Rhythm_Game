using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnBeat;
    public UnityEvent OnNote;
    public UnityEvent CatchNote;

    public static GameManager Instance;

    private List<int> MainChart = new List<int>();
    private List<int> MusicChart = new List<int> 
    {
        0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,
        0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,1,0,1,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0
    };

    private List<int> DelayChart = new List<int> {0 };
   
    public GameObject[] notePoints;

    public GameObject noteSyncPoint;

    public float BPM = 210;
    private float interval;     //time between beat that calculated  by BPM
    private float margin = 0.342f;
    private float inputError;
    private float timer;
    private bool scoreChance = false;

    public int count = 0;       //count of called beats

    //valriables for manipulate the starttime
    private bool BeatStart =false;
    [SerializeField] float startDelay = 0f;

    public int Score = 0;

    public int beatJump = 2;    //number of beats to move ingredients

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
        for (int i = 0; i < beatJump; i++) 
        {
            DelayChart.Add(0);
        }
        MainChart.AddRange(DelayChart);
        MainChart.AddRange(MusicChart);

        StartCoroutine(NoteStartDelay());
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
        if (BeatStart)
        {
            if (Time.time - timer >= interval)
            {
                if (MainChart[count] == 1)
                {
                    OnNote.Invoke();
                }
                OnBeat.Invoke();

                ++count;
                timer = Time.time;
                if (MainChart[count - 1] == 0)
                {
                }

            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                inputError = Time.time - timer;

                if (inputError < interval / 2)
                {
                    if (inputError <= margin && MusicChart[count - 2] == 1)
                    {
                        ++Score;
                        CatchNote.Invoke();
                    }
                }
                else
                {
                    if (inputError > interval - margin && MusicChart[count - 1] == 1)
                    {
                        ++Score;
                        CatchNote.Invoke();
                    }
                }
            }
        }

        
    }
    IEnumerator NoteStartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        timer = Time.time;
        BeatStart = true;
    }
}
