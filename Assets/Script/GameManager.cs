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
using System;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using static GameManager;
using FMODUnity;

public class GameManager : MonoBehaviour
{
    public UnityEvent OnBeat;
    public UnityEvent OnNote;
    public UnityEvent CatchNote;
    public UnityEvent FistMiss;

    //public UnityEvent OnNote_forMold;
    public UnityEvent FillMiss;

    public UnityEvent OnNote_3;
    public UnityEvent OnSpawnIngre;

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
        //0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,
        //1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,
        //0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,1,0,1,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,0,0,0,0,
        ////1-2 start
        //1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,0,0,
        //0,0,0,0,1,0,0,0,1,0,0,0,1,1,0,0,1,1,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,0,0,0,0,0,0,0,0,0
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,0,1,1,0,0,1,1,0,0,1,0,0,0,1,0,1,0,1,0,0,0,
        1,0,1,0,1,0,0,0,1,0,0,0,1,0,0,0,1,1,0,0,1,1,0,0,1,0,1,0,1,0,0,1,0,0,1,0,0,0,1,0,0,0,1,0,
        0,0,1,0,0,0,1,1,1,0,1,0,0,0,1,1,0,0,1,0,0,0,1,0,1,0,0,0,0,0,1,0,1,0,0,0,0,0,1,0,0,0,1,0,
        0,0,0,0,0,0,0,0,0,0, // next pattern
        1,0,0,1,0,0,0,0,1,0,0,1,0,0,0,1,1,1,0,0,0,1,1,0,0,0,1,0,0,0,0,0,1,0,0,0,1,0,0,0,1,0,0,0,
        1,0,0,0,1,0,0,0,1,0,0,0,0,0,0,0,1,0,1,0,0,0,1,1,0,0,1,1,0,0,1,0,1,0,1,0,0,0,1,0,0,0,0,0,
        0,0,1,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,0,0,0,0,0,0,0,0,0,0,1,0,0,0,
        1,0,0,0,1,0,0,0,1,0,0,0,1,0,1,1,0,0,0,0,1,0,1,1,0,0,0,0,1,0,0,1,0,0,1,0,0,0,0,1,0,0,0,0,
        1,0,0,0,1,0,0,0,1,1,1,0,0,0,1,1,0,0,0,1,0,0,0,0,0,0,0,1,0,0,0,0,1,0,0,0,1,0,1,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0
    };

    private List<int> DelayChart = new List<int> { 0, 0 };

    public float BPM = 283;//210;
    private float interval;     //time between beat that calculated  by BPM

    bool backgroundMoved = false;
    bool stageEnd = false;
    //value for judge
    private float margin_perfect = 0.056f;
    public float margin_good = 0.03f;
    public float scoreTimer;
    private bool isScoreGet = true;
    private float catchDelay = 0.0001f;
    private bool isCatchable = true;

    public int count = 0;       //count of called beats

    //valriables for manipulate the starttime
    private bool BeatStart = false;
    [SerializeField] float startDelay = 0f;
    [SerializeField] float bgmStartDelay = 1f;

    public int Score = 0;

    public int beatJump = 3;    //number of beats to move ingredients

    //value for 1-2
    public GameObject BackGround1;
    public GameObject BackGround2;
    public bool isStage1_2 = false;
    public GameObject ingredientManager;
    public GameObject moldManager;

    private enum catchState { Miss = 0, Perfect = 1, good = 2 };
    public int currentState = (int)catchState.Miss;


    public NoteManager noteManager;

    [SerializeField] GameObject fade;
    bool fadeOutStart = false;

   

    void Awake()
    {
        BeatTracker.OnFixedBeat += IterateChart;
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
        //StartCoroutine(BGMStartDelay());
        

    }

   

    IEnumerator FadeOutToNextScene(string sceneName)
    {
        fade.SetActive(true);
        fade.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }

    void Fade()
    {
        fade.SetActive(true);
        fade.GetComponent<Animator>().SetTrigger("FadeOut");
        fade.SetActive(false);
    }


    

    // Update is called once per frame
    void Update()
    {

        if (!FindObjectOfType<PauseMenu>().isPlaying)
        {
            return;
        }

        if (BeatStart && !stageEnd)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow) && isCatchable)
            {
                if (currentState == (int)catchState.good)
                {
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.notePress);
                    ++Score;
                    CatchNote.Invoke();

                    if (true)
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
        
        //ok
        if (count >= 144)//151)
        {
            isStage1_2 = true;
            //textEffectObj.transform.position = new Vector3(-7.32f, -3.6f, 0f);
        }
        //ending
        if (count == 386 && !stageEnd)//SpawnChart.Count - 1 && !stageEnd)
        {
            stageEnd = true;
            AudioManager.Instance.stageSource.Stop();
            if (Score > 75 * 7.5 && !fadeOutStart)
            {
                StartCoroutine(FadeOutToNextScene("CatHappy"));
            }
            else if (Score <= 75 * 7.5 && !fadeOutStart)
            {
                StartCoroutine(FadeOutToNextScene("CatAngry"));

            }
        }

        if (isStage1_2 && !backgroundMoved)
        {
            StartCoroutine(MoveBackground(0.2f));
        }

    }

    //Made IterateChart function and subscribed to BeatTracker.OnFixedBeat in the Awake function
    //On every beat IterateChart is called
    private void IterateChart()
    {
        if (!FindObjectOfType<PauseMenu>().isPlaying)
        {
            return;
        }

        if (BeatStart && !stageEnd)
        {
            if (SpawnChart[count] == 1)
            {
                noteNumber++;
                OnNote.Invoke();
                
            }
            OnBeat.Invoke();

            if (count + 4 <= SpawnChart.Count - 1)
            {
                if (SpawnChart[count + 4] == 1)
                {
                    noteNumber2++;
                    OnSpawnIngre.Invoke();

                }
            }


            ++count;
        }
        if (JudgeChart[count + 1] == 1)
        {
            judgeNumber++;
            //scoreTimer = BeatTracker.GetCurrentTime() + BeatTracker.GetBeatInterval() - margin_good;

            StartCoroutine(DefaultCycle());

            isScoreGet = false;

            //if (isStage1_2)
            //{
            //    OnNote_forMold.Invoke();
            //}
        }


    }
    IEnumerator MoveBackground(float duration)
    {
        backgroundMoved = true;
        float elapsedTime = 0f;
        //while (elapsedTime < duration)
        //{
        //    elapsedTime += Time.deltaTime;
        //    BackGround1.transform.position = Vector3.Lerp(Vector3.zero, new Vector3(-19.2f, 0f, 0f), elapsedTime / duration);
        //    yield return null;
        //}
        //Fade();
        yield return new WaitForSeconds(0.1f);
        BackGround1.SetActive(false);
        BackGround2.SetActive(true);   
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
        BeatStart = true;
    }

    private void OnDestroy()
    {
        BeatTracker.OnFixedBeat -= IterateChart;
    }


    //IEnumerator BGMStartDelay()
    //{
    //    yield return new WaitForSeconds(bgmStartDelay);
    //    AudioManager.Instance.PlayStageMusic(AudioManager.Instance.stage1);
    //}

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

    IEnumerator DefaultCycle()
    {
        yield return new WaitForSeconds(interval - margin_good);
        currentState = (int)catchState.good;
        yield return new WaitForSeconds(2*margin_good);
        if (currentState != 0) 
        {
            currentState = (int)catchState.Miss;
        }
        
    }
}





