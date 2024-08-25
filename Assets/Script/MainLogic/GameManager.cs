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
    [HideInInspector] public enum numberofStage { _1Hamster = 1, _2Cat = 2, _3Capybara = 3, _4Panda = 4, _5Lion = 5 };

    public numberofStage stageNumber = numberofStage._1Hamster;
    DataStorage dataStorage = new DataStorage();
    StageData stageData;
    EventAdapter eventAdapter;

    [Header("이펙트")]
    public Animator missText;
    public Animator goodText;
    public Animator perfectText;

    [HideInInspector]public static GameManager Instance;
    [HideInInspector] public int noteNumber = 0;
    [HideInInspector] public int noteNumber2 = 0;
    [HideInInspector] public int judgeNumber = 0;

    //public GameObject anyKeyObj;
    private BeatTracker beatTracker;
    private int startDelayBeatCount = 0;

    private int combo = 0;
    private int randomvalue = 0;
    private List<int> noterotationList = new List<int> {0 };
    private List<KeyCode> keyCodeList = 
        new List<KeyCode> {KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.LeftArrow };


    private List<int> SpawnChart = new List<int>();
    private List<int> MusicChart;
    private List<int> DelayChart = new List<int> { };

    [HideInInspector] public float BPM;
    private float interval;     //time between beat that calculated  by BPM

    bool backgroundMoved = false;
    bool stageEnd = false;
    //value for judge
    private float margin_perfect = 0.05f;
    private float margin_good = 0.1f;
    private bool isScoreGet = true;
    private float catchDelay = 0.01f;
    private bool isCatchable = true;

    public int count = 0;       //count of called beats

    //valriables for manipulate the starttime
    private bool BeatStart = false;
    [SerializeField] float startDelay = 0f;
    //[SerializeField] float bgmStartDelay = 1f;

    public int Score = 0;
    public int noteBeatInterval = 5;    //number of beats to move ingredients

    [HideInInspector] public int currentStage = 0;

    public enum catchState { Miss = 0, Perfect = 1, good = 2 };
    public catchState currentState = catchState.Miss;
    public NoteManager noteManager;

    //[SerializeField] GameObject fade;
    [SerializeField] Animator fadeanim;
    bool fadeOutStart = false;


    public List<GameObject> subStages = new List<GameObject>();


    void Awake()
    {
        BeatTracker.OnFixedBeat += IterateChart;    //FMOD 차트 구독
        if (Instance == null) { Instance = this; }  //singleton
        else { Destroy(gameObject); }
        
        eventAdapter = GetComponent<EventAdapter>();
        noteManager = FindObjectOfType<NoteManager>();
        beatTracker = FindObjectOfType<BeatTracker>();

        stageData = dataStorage.getStageData((int)stageNumber);

        BPM = stageData.BPM;
        MusicChart = stageData.MusicChart;

        isScoreGet = true;
        interval = 60 / BPM;


        for (int i = 0; i < noteBeatInterval; i++)
        {
            DelayChart.Add(0);
        }
        SpawnChart.AddRange(DelayChart);
        SpawnChart.AddRange(MusicChart);

        StartCoroutine(NoteStartDelay());

        noteManager.DirectionChange(stageData.noteDirection[currentStage]);

        
    }

    IEnumerator FadeOutToNextScene(string sceneName)
    {
        //fade.SetActive(true);
        //fade.GetComponent<Animator>().SetTrigger("FadeOut");
        fadeanim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator FadeInOut()
    {
        fadeanim.SetTrigger("FadeOut");
        yield return new WaitForSeconds(1f);
        subStages[currentStage].SetActive(false);
        subStages[currentStage + 1].SetActive(true);
        currentStage++;
        noteManager.spawnPointChange(currentStage);
        noteManager.DirectionChange(stageData.noteDirection[currentStage]);
        fadeanim.SetTrigger("FadeIn");
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
            if (Input.GetKeyDown(KeyCode.DownArrow)|| Input.GetKeyDown(KeyCode.RightArrow)
                || Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.LeftArrow)
                ) 
            {
                if (!Input.GetKeyDown(keyCodeList[noterotationList[judgeNumber]]) || currentState == catchState.Miss)
                {
                    eventAdapter.Event_MissNote();
                    missText.SetTrigger("Miss");
                    noteManager.NoteJudgeEffect("Miss");
                    combo = 0;
                    StartCoroutine(CatchDelay());
                }
                else
                {
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.notePress);
                    ++Score;
                    eventAdapter.Event_CatchNote(); //노트캐치

                    if (currentState == catchState.Perfect)
                    {
                        perfectText.SetTrigger("Perfect");
                        noteManager.NoteJudgeEffect("Perfect");
                        if (combo > 5)
                        {
                            Score += 13;
                        }
                        else
                        {
                            Score += 10;
                        }
                        combo++;
                    }
                    else
                    {
                        goodText.SetTrigger("Good");
                        noteManager.NoteJudgeEffect("Good");
                        Score += 5;
                        combo = 0;
                    }
                    isScoreGet = true;

                }
                
            }
        }

        //stage change
        if (currentStage!= stageData.stageCount-1 && count == stageData.stageChangeBeats[currentStage])
        {
            StartCoroutine(FadeInOut());

            
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

            if (count + 6 <= SpawnChart.Count - 1)
            {
                if (SpawnChart[count + 6 ] == 1)
                {
                    noteNumber2++;
                    randomvalue = RandomMachine();
                    noterotationList.Add(randomvalue);
                    noteManager.EventNoteSpawn(randomvalue);

                }
            }
            ++count;
        }
        if (count + 1 <= SpawnChart.Count - 1 && SpawnChart[count + 1] == 1)
        {           
            StartCoroutine(DefaultCycle());
        }


    }
    IEnumerator MoveBackground(float duration)
    {
        backgroundMoved = true;
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            //BackGround.transform.position = Vector3.Lerp(Vector3.zero, new Vector3(-19.2f, 0f, 0f), elapsedTime / duration);
            yield return null;
        }
    }

    private int RandomMachine()
    {
        int randomvalue = UnityEngine.Random.Range(0, stageData.noteRotationStageList[currentStage].Length);
        return stageData.noteRotationStageList[currentStage][randomvalue];
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


    

    IEnumerator CatchDelay()
    {
        isCatchable = false;
        
        //yield return new WaitForSeconds(catchDelay);
        yield return null;
        isCatchable = true;
    }

    IEnumerator DefaultCycle()
    {
        yield return new WaitForSeconds(interval - margin_good);
        judgeNumber++;
        currentState = catchState.good;
        isScoreGet = false;

        yield return new WaitForSeconds(margin_good - margin_perfect);
        currentState = catchState.Perfect;

        yield return new WaitForSeconds(2*margin_perfect);
        currentState = catchState.good;


        yield return new WaitForSeconds(margin_good);
        currentState = catchState.Miss;
    }


}





