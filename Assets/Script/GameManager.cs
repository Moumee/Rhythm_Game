using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.Video;
using AsyncOperation = UnityEngine.AsyncOperation;

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

    public Animator fadeAnim;

    public static GameManager Instance;

    public int noteNumber = 0;
    public int noteNumber2 = 0;
    public int judgeNumber = 0;


    public GameObject anyKeyObj;

    [SerializeField] SceneController sceneController;

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

    private List<int> DelayChart = new List<int> { 0, 0 };

    public GameObject[] notePoints;

    public GameObject noteSyncPoint;

    public float BPM = 210;
    private float interval;     //time between beat that calculated  by BPM
    private float timer;

    bool backgroundMoved = false;
    bool stageEnd = false;
    bool skipAvailable = false;
    bool success = false;
    bool fail = false;
    //value for judge
    private float margin_perfect = 0.056f;
    public float margin_good = 0.042f;
    public float scoreTimer;
    private bool isScoreGet = true;
    private float catchDelay = 0.05f;
    private bool isCatchable = true;

    public int count = 0;       //count of called beats

    //valriables for manipulate the starttime
    private bool BeatStart = false;
    [SerializeField] float startDelay = 0f;
    [SerializeField] float bgmStartDelay = 1f;

    public int Score = 0;

    public int beatJump = 4;    //number of beats to move ingredients

    //value for 1-2
    public GameObject BackGround;
    public bool isStage1_2 = false;
    public GameObject ingredientManager;
    public GameObject moldManager;

    private double dspStartTime;
    private double elapsedDSPTime;


    public NoteManager noteManager;
    AsyncOperation successScene;
    AsyncOperation failScene;

    AudioSource stageSource;

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        noteManager = FindObjectOfType<NoteManager>();
        //ingredientManager = FindObjectOfType<IngredientManager>();  
        //moldManager = FindObjectOfType<MoldManager>();  
        stageSource = AudioManager.Instance.stageSource;

        isScoreGet = true;
        interval = 60 / BPM;
        margin_good = 0.114f;

        SpawnChart.AddRange(DelayChart);
        SpawnChart.AddRange(MusicChart);
        for (int i = 0; i < beatJump * 2; i++)
        {
            DelayChart.Add(0);
        }
        JudgeChart.AddRange(DelayChart);
        JudgeChart.AddRange(MusicChart);



        StartCoroutine(NoteStartDelay());
        StartCoroutine(BGMStartDelay());


    }

    
    // Update is called once per frame
    void Update()
    {
        if (!FindObjectOfType<PauseMenu>().isPlaying)
        {
            return;
        }

        //if (Input.GetKeyDown(KeyCode.DownArrow)) { Debug.Log((AudioManager.Instance.stageSource.time)); }

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

                if (count + 4 <= SpawnChart.Count - 1)
                {
                    if (SpawnChart[count + 4] == 1)
                    {
                        noteNumber2++;
                        OnNote_3.Invoke();

                    }
                }


                ++count;
                timer = Time.time;
                if (JudgeChart[count + 1] == 1)
                {
                    judgeNumber++;
                    scoreTimer = Time.time + interval - margin_good;

                    isScoreGet = false;

                    if (isStage1_2)
                    {
                        OnNote_forMold.Invoke();
                    }
                }



            }


            if (Input.GetKeyDown(KeyCode.DownArrow) && isCatchable)
            {
                if (Time.time >= scoreTimer &&
                    Time.time < scoreTimer + margin_good * 2 && !isScoreGet)
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
                    if (Time.time >= scoreTimer + margin_good - margin_perfect
                        && Time.time <= scoreTimer + margin_good + margin_perfect)
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

        if (Time.time > scoreTimer + 2 * margin_good && !isScoreGet && !stageEnd)
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
            //if (BackGround.transform.position.x >= -19.2f)
            //{
            //    BackGround.transform.position += Vector3.left * 40f * Time.deltaTime;
            //}
            textEffectObj.transform.position = new Vector3(-7.32f, -3.6f, 0f);
        }

        if (count == SpawnChart.Count - 1 && !stageEnd)
        {
            stageEnd = true;
            AudioManager.Instance.stageSource.Stop();
            if (Score > 75 * 7.5)
            {
                sceneController.LoadScene("HamsterHappy");
            }
            else
            {
                sceneController.LoadScene("HamsterAngry");


            }
        }

        if (isStage1_2 && !backgroundMoved)
        {
            StartCoroutine(MoveBackground(0.2f));
        }

    }

    IEnumerator MoveBackground(float duration)
    {
        backgroundMoved = true;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            BackGround.transform.position = Vector3.Lerp(Vector3.zero, new Vector3(-19.2f, 0f, 0f), elapsedTime / duration);
            yield return null;
        }
    }


    //IEnumerator PreLoadScene(string sceneName)
    //{
    //    asyncOperation = SceneManager.LoadSceneAsync(sceneName);
    //    asyncOperation.allowSceneActivation = false;

    //    while (!asyncOperation.isDone)
    //    {
    //        yield return null;
    //    }
    //}
    IEnumerator NoteStartDelay()
    {
        yield return new WaitForSeconds(startDelay);
        timer = Time.time;

        BeatStart = true;
    }



    IEnumerator BGMStartDelay()
    {
        yield return new WaitForSeconds(bgmStartDelay);
        AudioManager.Instance.PlayStageMusic(AudioManager.Stage.Hamster);
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




