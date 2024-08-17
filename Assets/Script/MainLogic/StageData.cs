using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StageData
{
    public List<int> MusicChart { get; }//Ã¤º¸
    public float BPM { get; }//
    public string successScene { get; }
    public string failScene { get; }
    public int stageCount { get; }
    public List<int> stageChangeBeats { get; }



    public StageData(List<int> chart, float bpm, string success, string fail, int count, List<int> changeBeats)
    {
        MusicChart = chart;
        BPM = bpm;
        successScene = success;
        failScene = fail;
        stageCount = count;
        stageChangeBeats = changeBeats;
    }
}


