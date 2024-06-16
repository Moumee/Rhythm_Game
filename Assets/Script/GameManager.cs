using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnBeat;
    public UnityEvent OnNote;
    public UnityEvent CatchNote;
    public UnityEvent FistMiss;


    public UnityEvent OnBeat_2;
    public UnityEvent OnNote_2;
    public UnityEvent OnNote_forMold;
    public UnityEvent CatchNote_2;
    public UnityEvent FillMiss;

    public UnityEvent OnNote_3;

    public Animator missText;
    public Animator goodText;
    public Animator perfectText;
    public GameObject textEffectObj;

    public static GameManager Instance;

    public int noteNumber = 0;
    public int noteNumber2 = 0;
    public int judgeNumber = 0;

    public VideoPlayer successPlayer;
    public VideoPlayer failedPlayer;
    public RawImage videoHolder;
    bool videoStarted = false;

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

    bool stageEnd = false;  
    //value for judge
    private float margin_perfect = 0.056f;
    public float margin_good = 0.042f;
    public float scoreTimer;
    private bool isScoreGet = true;
    private float catchDelay = 0.1f;
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
    public GameObject ingredientManager;
    public GameObject moldManager;



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
        //ingredientManager = FindObjectOfType<IngredientManager>();  
        //moldManager = FindObjectOfType<MoldManager>();  

        isScoreGet = true;
        interval = 60 / BPM;
        margin_good = 0.114f;

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
        if (BeatStart && !stageEnd)
        {   
            if (Time.time - timer >= interval)
            {
                if (SpawnChart[count] == 1)
                {
                    noteNumber++;
                    if (isStage1_2)
                    {
                        OnNote_2.Invoke();
                    }
                    else
                    {
                        OnNote.Invoke();
                    }
                }
                if (isStage1_2)
                {
                    OnBeat_2.Invoke();
                }
                else
                {
                    OnBeat.Invoke();
                }

                if (SpawnChart[count+4] == 1)
                {
                    noteNumber2++;
                    OnNote_3.Invoke();

                }

                    ++count;
                timer = Time.time;
                if (JudgeChart[count + 1] == 1)
                {
                    judgeNumber++;
                    scoreTimer = timer + interval - margin_good;

                    isScoreGet = false;

                    if (isStage1_2)
                    {
                        OnNote_forMold.Invoke();
                    }
                }

                

            }

            
            if (Input.GetKeyDown(KeyCode.DownArrow)&&isCatchable)
            {
                if(Time.time >= scoreTimer && Time.time < scoreTimer + margin_good*2 && !isScoreGet ) 
                {
                    ++Score;
                    if (isStage1_2)
                    {
                        CatchNote_2.Invoke();
                    }
                    else
                    {
                        CatchNote.Invoke();
                    }
                    if (Time.time >= scoreTimer+margin_good-margin_perfect && Time.time <= scoreTimer + margin_good + margin_perfect)
                    {
                        perfectText.SetTrigger("Perfect");
                        noteManager.NoteJudgeEffect("Perfect");
                        Score += 10;

                    }
                    else
                    {
                        goodText.SetTrigger("Good");
                        noteManager.NoteJudgeEffect("Good");
                        Score += 5;
                    }
                    isScoreGet = true;
                    
                }
                else
                {
                    missText.SetTrigger("Miss");
                    noteManager.NoteJudgeEffect("Miss");
                    StartCoroutine(CatchDelay());
                }
            }
        }

        if (Time.time > scoreTimer + 2*margin_good && !isScoreGet && !stageEnd)
        {
            isScoreGet = true;
            missText.SetTrigger("Miss");
            noteManager.NoteJudgeEffect("Miss");
            if (isStage1_2)
            {
                FillMiss.Invoke();
            }
        }
        
        if (count >= 151)
        {
            isStage1_2 = true;

            //SceneManager.LoadSceneAsync(2);
            if(BackGround.transform.position.x >= -19.2f)
            {
                BackGround.transform.position += Vector3.left * 30f*Time.deltaTime;
            }
            textEffectObj.transform.position = new Vector3(-7.2f, -3.6f, 0f);
        }

        if (count >= SpawnChart.Count && !videoStarted)
        {
            stageEnd = true;
            AudioManager.Instance.bgmSource.Stop();
            videoStarted = true;
            if (Score > 75 * 5)
            {
                StartCoroutine(videoLoopLength(successPlayer));

            }
            else
            {
                StartCoroutine(videoLoopLength(failedPlayer));

            }
        }
        
        if (successPlayer.frame == 2 || failedPlayer.frame == 2)
        {
            videoHolder.color = new Color(1, 1, 1, 1);
            
        }

        
    }

    IEnumerator videoLoopLength(VideoPlayer vp)
    {
        vp.Play();
        if (vp == successPlayer)
        {
            AudioManager.Instance.PlaySFX(AudioManager.SFX.Success);
            AudioManager.Instance.PlaySFX(AudioManager.SFX.SuccessEffect);
        }
        else if (vp == failedPlayer)
        {
            AudioManager.Instance.PlaySFX(AudioManager.SFX.Fail);
        }
        yield return new WaitForSeconds(6f);
        vp.Stop();
        if (vp == successPlayer)
            SceneManager.LoadSceneAsync("HamsterHappy");
        else if (vp == failedPlayer)
            SceneManager.LoadSceneAsync("HamsterAngry");
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
        if (!isStage1_2)
        {
            FistMiss.Invoke();
        }
        yield return new WaitForSeconds(catchDelay);
        isCatchable = true;
    }
}
