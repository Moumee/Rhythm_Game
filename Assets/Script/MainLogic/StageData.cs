using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct StageData
{
    public List<int> MusicChart { get; }
    public float BPM { get; }
    public string successScene{ get; }
    public string failScene{ get; }


    public StageData(List<int> chart, float bpm, string success, string fail)
    {
        MusicChart = chart;
        BPM = bpm;
        successScene = success;
        failScene = fail;
    }
}


