using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StageData
{
    public List<int> MusicChart { get; }//채보
    public float BPM { get; }//
    public string successScene { get; }
    public string failScene { get; }
    public int stageCount { get; }
    public List<int> stageChangeBeats { get; }
    public List<int> noteDirection { get; }
    public int[][] noteRotationStageList { get; }
    public int noteInterval { get; }

    public int oneCount { get; } //노트 차트의 1의 개수



    public StageData
    (List<int> chart, float bpm, string success, string fail, int count, 
        List<int> changeBeats, List<int> direction, int[][] rotation,
        int interval, int numOfOnes)
    {
        MusicChart = chart;
        BPM = bpm;
        successScene = success;
        failScene = fail;
        stageCount = count;
        stageChangeBeats = changeBeats;
        noteDirection = direction;
        noteRotationStageList = rotation;
        noteInterval = interval;
        oneCount = numOfOnes;
    }
}


