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
        0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,1,0,1,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,
        //1-2 start
        1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,
        0,0,0,0,1,0,0,0,1,0,0,0,1,1,0,0,1,1,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0
    };

    private List<int> DelayChart = new List<int> {0,0};
   
    public GameObject[] notePoints;

    public GameObject noteSyncPoint;

    public float BPM = 210;
    private float interval;     //time between beat that calculated  by BPM
    private float timer;

    //value for judge
    private float margin_perfect = 0.057f;
    public float margin_good = 0.2f;
    public float scoreTimer;
    private bool isScoreGet = true;
    private float catchDelay = 0.5f;
    private bool isCatchable = true;

    public int count = 0;       //count of called beats

    //valriables for manipulate the starttime
    private bool BeatStart =false;
    [SerializeField] float startDelay = 0f;
    [SerializeField] float bgmStartDelay = 1f;

    public int Score = 0;

    public int beatJump = 4;    //number of beats to move ingredients

    //value for 1-2
    public GameObject BackGround;
    public bool isStage1_2 = false;
    public GameObject IngredientManager;
    public GameObject MoldManager;



    public NoteManager noteManager;

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

        noteManager = FindObjectOfType<NoteManager>();  

        isScoreGet = true;
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
                if (JudgeChart[count + 1] == 1)
                {
                    scoreTimer = timer + interval - margin_good;

                    isScoreGet = false;
                }

            }

            
            if (Input.GetKeyDown(KeyCode.Space)&&isCatchable)
            {
                if(Time.time >= scoreTimer && Time.time < scoreTimer + margin_good*2 && !isScoreGet ) 
                {
                    ++Score;
                    CatchNote.Invoke();
                    if (Time.time >= scoreTimer+margin_good-margin_perfect && Time.time < scoreTimer + margin_good + margin_perfect)
                    {
                        perfectText.SetTrigger("Perfect");
                        noteManager.NoteJudgeEffect("Perfect");

                    }
                    else
                    {
                        goodText.SetTrigger("Good");
                        noteManager.NoteJudgeEffect("Good");
                    }
                    isScoreGet = true;
                    
                }
                else
                {
                    missText.SetTrigger("Miss");
                    noteManager.NoteJudgeEffect("Miss");
                }
                StartCoroutine(CatchDelay());
            }
        }
        
        if (count >= 5)//152)
        {
            isStage1_2 = true;
            //SceneManager.LoadSceneAsync(2);
            if(BackGround.transform.position.x >= -19.2f)
            {
                BackGround.transform.position += Vector3.left * 30f*Time.deltaTime;
            }
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

    IEnumerator CatchDelay()
    {
        isCatchable = false;
        yield return new WaitForSeconds(catchDelay);
        isCatchable = true;
    }
}
