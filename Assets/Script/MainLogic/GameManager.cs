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
    //스테이지 변수
    [HideInInspector] public enum numberofStage { _1Hamster = 1, _2Cat = 2, _3Capybara = 3, _4Panda = 4, _5Lion = 5 };
    public numberofStage stageNumber = numberofStage._1Hamster;
    DataStorage dataStorage = new DataStorage();
    StageData stageData;
    [HideInInspector] public int currentStage = 0;
    public List<GameObject> subStages = new List<GameObject>();


    EventAdapter eventAdapter;

    [Header("이펙트")]
    public Animator missText;
    public Animator goodText;
    public Animator perfectText;
    public Animator comboText;

    [HideInInspector]public static GameManager Instance;
    [HideInInspector] public int noteNumber = 0;
    [HideInInspector] public int noteSerialNum = 0;
    [HideInInspector] public int judgeNumber = 0;

    //public GameObject anyKeyObj;
    private BeatTracker beatTracker;

    private int combo = 0;

    //노트관련
    NoteManager noteManager;
    public enum catchState { Miss = 0, Perfect = 1, good = 2 };
    public catchState currentState = catchState.Miss;
    private int randomvalue = 0;
    private List<int> noterotationList = new List<int> {0};
    private List<KeyCode> keyCodeList =
        new List<KeyCode> { KeyCode.DownArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.LeftArrow };

    

    //채보관련
    private List<int> SpawnChart = new List<int>();
    private List<int> MusicChart;
    private List<int> DelayChart = new List<int> { };

    //음악관련
    [HideInInspector] public float BPM;
    private float interval;     //time between beat that calculated  by BPM

    

    //value for judge
    private float margin_perfect = 0.05f;
    private float margin_good = 0.1f;
    private bool isMissDelay = false;
    private bool isCatchable = true;

    public int count = 0;       //count of called beats

    //valriables for manipulate the starttime
    private bool BeatStart = false;
    bool stageEnd = false;
    [SerializeField] float startDelay = 1f;
    

    public int Score = 0;
    
    [HideInInspector] public int missCount;
    public int ingreDelay = 4;
    public int noteBeatInterval = 5;    //number of beats to move ingredients




    //페이드 변수
    //[SerializeField] GameObject fade;
    public UnityEngine.UI.Image transitionImage;
    bool fadeOutStart = false;
    public float transitionDuration = 1f;
    TextEffectMove textEffectMove;
    private Canvas canvas;
    private RectTransform canvasRectTransform;
    public Canvas targetCanvas;



    void Awake()
    {
        BeatTracker.OnFixedBeat += IterateChart;    //FMOD 차트 구독

        canvas = FindObjectOfType<Canvas>();

        if (Instance == null) { Instance = this; }  //singleton
        else { Destroy(gameObject); }
        
        eventAdapter = GetComponent<EventAdapter>();
        noteManager = FindObjectOfType<NoteManager>();
        beatTracker = FindObjectOfType<BeatTracker>();
        textEffectMove = FindObjectOfType<TextEffectMove>();

        stageData = dataStorage.getStageData((int)stageNumber);
        noteBeatInterval = stageData.noteInterval;

        BPM = stageData.BPM;
        MusicChart = stageData.MusicChart;

        interval = 60 / BPM;


        for (int i = 0; i < noteBeatInterval; i++)
        {
            DelayChart.Add(0);
        }
        SpawnChart.AddRange(DelayChart);
        SpawnChart.AddRange(MusicChart);

        StartCoroutine(NoteStartDelay());

        noteManager.DirectionChange(stageData.noteDirection[currentStage]);
        textEffectMove.EffectMove(currentStage);

        //CrossFade Image Generator
        targetCanvas = FindObjectOfType<Canvas>();
        GameObject imageObject = new GameObject("TransitionImage");
        imageObject.transform.SetParent(targetCanvas.transform, false);
        transitionImage = imageObject.AddComponent<UnityEngine.UI.Image>();
        RectTransform rectTransform = transitionImage.GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.sizeDelta = Vector2.zero;
        rectTransform.anchoredPosition = Vector2.zero;
        transitionImage.transform.SetAsLastSibling();
        transitionImage.raycastTarget = false;
        transitionImage.gameObject.SetActive(false);
    }

    

    IEnumerator CrossfadeSubstage()
    {
        yield return new WaitForEndOfFrame();
        Texture2D screenCapture = ScreenCapture.CaptureScreenshotAsTexture();
        if (transitionImage != null)
        {
            transitionImage.gameObject.SetActive(true);
            transitionImage.sprite = Sprite.Create(screenCapture, new Rect(0, 0, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f));
            transitionImage.color = Color.white;
            transitionImage.raycastTarget = false;
            
        }
        subStages[currentStage-1].SetActive(false);
        subStages[currentStage].SetActive(true);
        textEffectMove.EffectMove(currentStage);
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = 1f - (elapsedTime / transitionDuration);
            transitionImage.color = new Color(1f, 1f, 1f, alpha);
            yield return null;
        }
        transitionImage.gameObject.SetActive(false);
        Destroy(screenCapture);
        Debug.Log(currentStage);
        noteManager.spawnPointChange(currentStage);
        noteManager.DirectionChange(stageData.noteDirection[currentStage]);
    }
    IEnumerator SlideBackground(float duration)
    {
        float elapsedTime = 0f;
        //subStages[currentStage].transform.position = Vector3.right * 19.2f;
        subStages[currentStage].SetActive(true);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            subStages[currentStage-1].transform.position = Vector3.Lerp(Vector3.zero, new Vector3(-19.2f, 0f, 0f), elapsedTime / duration);
            subStages[currentStage].transform.position = Vector3.Lerp(new Vector3(19.2f, 0f, 0f), Vector3.zero, elapsedTime / duration);
            yield return null;
        }
        textEffectMove.EffectMove(currentStage);
        noteManager.spawnPointChange(currentStage);
        noteManager.DirectionChange(stageData.noteDirection[currentStage]);
        subStages[currentStage - 1].SetActive(false);
    }


    void IntervalFix()
    {

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
            if ((Input.GetKeyDown(KeyCode.DownArrow)|| Input.GetKeyDown(KeyCode.RightArrow)
                || Input.GetKeyDown(KeyCode.UpArrow)|| Input.GetKeyDown(KeyCode.LeftArrow))
                ) 
            {
                
                if (Input.GetKeyDown(keyCodeList[noterotationList[judgeNumber]]) && currentState != catchState.Miss )
                {
                    
                    isCatchable = false;
                    AudioManager.Instance.PlaySFX(AudioManager.Instance.notePress);
                    
                    eventAdapter.Event_CatchNote(currentState == catchState.Perfect, noterotationList[judgeNumber]); //노트캐치

                    if (currentState == catchState.Perfect)
                    {
                        
                        noteManager.NoteJudgeEffect("Perfect");
                        if (combo > 5)
                        {
                            comboText.SetTrigger("Combo");
                            Score += 13;
                        }
                        else
                        {
                            perfectText.SetTrigger("Perfect");
                            Score += 10;
                        }
                        combo++;
                    }
                    else
                    {
                        noteManager.NoteJudgeEffect("Good");
                        goodText.SetTrigger("Good");
                        Score += 5;
                        combo = 0;
                    }
                    

                }
                else 
                {
                    currentState = catchState.Miss;
                    isCatchable = false;
                    eventAdapter.Event_MissNote();
                    if (!isMissDelay)
                    {
                        missCount++;
                        
                        missText.SetTrigger("Miss");
                        noteManager.NoteJudgeEffect("Miss");
                        combo = 0;
                    }
                        
                    
                }
                currentState = catchState.Miss;
                StartCoroutine(MissDelay());

            }
        }

        //stage change
        
        
        //ending
        if (count == SpawnChart.Count - 1 && !stageEnd)
        {
            stageEnd = true;
            AudioManager.Instance.stageSource.Stop();
            ScoreStorage.Instance.FinalScore += Score;
            if (Score > stageData.oneCount * 7.5 && !fadeOutStart)
            {
                ScoreStorage.Instance.isSuccess[currentStage-1] = true;
                SceneTransitionManager.LoadSceneWithTransition(stageData.successScene);
            }
            else if (Score <= stageData.oneCount * 7.5 && !fadeOutStart)
            {
                SceneTransitionManager.LoadSceneWithTransition(stageData.failScene);

            }
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

        if (currentStage != stageData.stageCount - 1 //&& currentStage == stageCheck
            && count == stageData.stageChangeBeats[currentStage])
        {
            currentStage++;
            if( stageNumber == numberofStage._1Hamster)
            {
                StartCoroutine(SlideBackground(1f));
            }
            else
            {
                StartCoroutine(CrossfadeSubstage());
            }
            
        }

        if (BeatStart && !stageEnd)
        {
            if (SpawnChart[count] == 1)
            {
                noteNumber++;
                eventAdapter.Event_OnNote();
            }
            eventAdapter.Event_OnBeat();

            if (count + noteBeatInterval+1 <= SpawnChart.Count - 1)
            {
                if (SpawnChart[count + noteBeatInterval+1] == 1)
                {
                    noteSerialNum++;
                    randomvalue = RandomMachine();
                    noterotationList.Add(randomvalue);
                    noteManager.EventNoteSpawn(randomvalue);

                }
            }
            if (count + ingreDelay <= SpawnChart.Count - 1)
            {
                if (SpawnChart[count + ingreDelay] == 1)
                {
                    eventAdapter.Event_SpawnIngre();

                }
            }

            ++count;
        }
        if (count + 1 <= SpawnChart.Count - 1 && SpawnChart[count + 1] == 1)
        {           
            StartCoroutine(DefaultCycle());
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
        FindObjectOfType<BeatTracker>().PlayMusic();
    }

    private void OnDestroy()
    {
        BeatTracker.OnFixedBeat -= IterateChart;
    }


    

    IEnumerator MissDelay()
    {
        isMissDelay = true;
        yield return new WaitForSeconds(interval/2);
        isMissDelay = false;
    }

    IEnumerator DefaultCycle()
    {
        yield return new WaitForSeconds(interval - margin_good);
        judgeNumber++;
        int tempJudgeNum = judgeNumber;
        currentState = catchState.good;
        isCatchable = true;

        yield return new WaitForSeconds(margin_good - margin_perfect);
        currentState = catchState.Perfect;

        yield return new WaitForSeconds(2*margin_perfect);
        currentState = catchState.good;


        yield return new WaitForSeconds(margin_good - margin_perfect);
        if(judgeNumber == tempJudgeNum)
        {
            currentState = catchState.Miss;
        }
        
    }


}





