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
    //public UnityEvent FistMiss;
    public UnityEvent FillMiss;
    public UnityEvent OnSpawnIngre;

    [HideInInspector]public enum numberofStage {_1Hamster = 1, _2Cat = 2, _3Capybara = 3, _4Panda = 4, _5Lion = 5};
    public numberofStage stageNumber = numberofStage._1Hamster;
    DataStorage dataStorage = new DataStorage();
    StageData stageData;


    [HideInInspector]public EventAdapter eventAdapter;
    private JudgePrinter judgePrinter;
    

    [Header("이펙트")]
    public Animator missText;
    public Animator goodText;
    public Animator perfectText;


    public static GameManager Instance;


    [HideInInspector]public int noteNumber = 0;
    [HideInInspector]public int noteNumber2 = 0;
    [HideInInspector]public int judgeNumber = 0;

    public GameObject anyKeyObj;

    [SerializeField] SceneController sceneController;


    private List<int> SpawnChart = new List<int>();
    private List<int> JudgeChart = new List<int>();


    private List<int> MusicChart;
    private List<int> DelayChart = new List<int> { 0, 0 };


    [HideInInspector]public float BPM;
    private float interval;     //time between beat that calculated  by BPM

    bool backgroundMoved = false;
    bool stageEnd = false;
    //value for judge
    private float margin_perfect = 0.025f;
    private float margin_good = 0.05f;
    private bool isScoreGet = true;
    private float catchDelay = 0.01f;
    private bool isCatchable = true;

    public int count = 0;       //count of called beats

    //valriables for manipulate the starttime
    private bool BeatStart = false;
    [SerializeField] float startDelay = 0f;
    //[SerializeField] float bgmStartDelay = 1f;

    public int Score = 0;

    public int beatJump = 3;    //number of beats to move ingredients

    //value for 1-2
    public GameObject BackGround;
    public int currentStage = 0;

    public enum catchState { Miss = 0, Perfect = 1, good = 2 };
    public int currentState = (int)catchState.Miss;


    public NoteManager noteManager;

    [SerializeField] GameObject fade;
    bool fadeOutStart = false;


    public GameObject firstSubStage;
    public GameObject secondSubStage;
    public GameObject thirdSubStage;


    void Awake()
    {
        BeatTracker.OnFixedBeat += IterateChart;    //FMOD 차트 구독
        if (Instance == null) { Instance = this; }  //singleton
        else { Destroy(gameObject); }
        
        eventAdapter = GetComponent<EventAdapter>();
        noteManager = FindObjectOfType<NoteManager>();

        stageData = dataStorage.getStageData((int)stageNumber);

        BPM = stageData.BPM;
        MusicChart = stageData.MusicChart;

        isScoreGet = true;
        interval = 60 / BPM;


        SpawnChart.AddRange(DelayChart);
        SpawnChart.AddRange(MusicChart);
        for (int i = 0; i < beatJump * 2; i++)
        {
            DelayChart.Add(0);
        }
        JudgeChart.AddRange(DelayChart);
        JudgeChart.AddRange(MusicChart);


        StartCoroutine(NoteStartDelay());
        

    }

    IEnumerator FadeOutToNextScene(string sceneName)
    {
        fade.SetActive(true);
        fade.GetComponent<Animator>().SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
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
                    eventAdapter.Event_CatchNote(); //노트캐치

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
                    eventAdapter.Event_MissNote();
                    missText.SetTrigger("Miss");
                    noteManager.NoteJudgeEffect("Miss");
                    StartCoroutine(CatchDelay());
                }
            }
        }
        
        //stage change
        if (count == 151)
        {
            currentStage = 1;
            //textEffectObj.transform.position = new Vector3(-7.32f, -3.6f, 0f);
            secondSubStage.SetActive(true);
            firstSubStage.SetActive(false);
        }
        if (count == 235)
        {
            currentStage = 2;
            secondSubStage.SetActive(false);
            thirdSubStage.SetActive(true);
        }
        //ending
        if (count == SpawnChart.Count - 1 && !stageEnd)
        {
            stageEnd = true;
            AudioManager.Instance.stageSource.Stop();
            if (Score > 75 * 7.5 && !fadeOutStart)
            {
                StartCoroutine(FadeOutToNextScene(stageData.successScene));
            }
            else if (Score <= 75 * 7.5 && !fadeOutStart)
            {
                StartCoroutine(FadeOutToNextScene(stageData.failScene));

            }
        }

        if (currentStage>0 && !backgroundMoved)
        {
            //StartCoroutine(MoveBackground(0.2f));
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
                eventAdapter.Event_OnNote();
            }
            eventAdapter.Event_OnBeat();

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
            StartCoroutine(DefaultCycle());

            isScoreGet = false;

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
        
        yield return new WaitForSeconds(catchDelay);
        isCatchable = true;
    }

    IEnumerator DefaultCycle()
    {
        yield return new WaitForSeconds(interval - margin_good);
        currentState = (int)catchState.good;

        yield return new WaitForSeconds(2*margin_good);
        currentState = (int)catchState.Miss;
    }
}





