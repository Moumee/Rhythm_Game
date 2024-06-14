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

    public Animator missText;
    public Animator goodText;
    public Animator perfectText;

    public static GameManager Instance;

    private List<int> SpawnChart = new List<int>();
    private List<int> JudgeChart = new List<int>();
    
    private List<int> MusicChart = new List<int> 
    {
        0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,
        0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,1,0,1,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0
    };

    private List<int> DelayChart = new List<int> {0,0};
   
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
    [SerializeField] float bgmStartDelay = 1f;

    public int Score = 0;

    public int beatJump = 4;    //number of beats to move ingredients

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        

        

        interval = 60 / BPM;

        SpawnChart.AddRange(DelayChart);
        SpawnChart.AddRange(MusicChart);
        for (int i = 0; i < beatJump*2; i++) 
        {
            DelayChart.Add(0);
        }
        JudgeChart.AddRange(DelayChart);
        JudgeChart.AddRange(MusicChart);

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            StartCoroutine(BGMStartDelay());
        }
        StartCoroutine(NoteStartDelay());
    }

    


    // Update is called once per frame
    void Update()
    {
        if (BeatStart)
        {
            if (Time.time - timer >= interval)
            {
                if (SpawnChart[count] == 1)
                {
                    OnNote.Invoke();
                }
                OnBeat.Invoke();
                

                ++count;
                timer = Time.time;
                if (SpawnChart[count - 1] == 0)
                {
                }

            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                inputError = Time.time - timer;

                if (inputError < interval / 2)
                {
                    if (inputError <= margin && JudgeChart[count] == 1)
                    {
                        ++Score;
                        CatchNote.Invoke();
                        perfectText.SetTrigger("Perfect");
                        
                    }
                }
                else
                {
                    if (inputError > interval - margin && JudgeChart[count] == 1)
                    {
                        ++Score;
                        CatchNote.Invoke();
                        goodText.SetTrigger("Good");
                    }
                }
            }
        }
        
        if (count == 155)
        {
            SceneManager.LoadSceneAsync(2);
        }

        
    }

    
    IEnumerator NoteStartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        timer = Time.time;
        BeatStart = true;
    }



    IEnumerator BGMStartDelay()
    {
        yield return new WaitForSeconds(bgmStartDelay);
        AudioManager.Instance.PlayBGM(AudioManager.BGM.Hamster);
    }
}
