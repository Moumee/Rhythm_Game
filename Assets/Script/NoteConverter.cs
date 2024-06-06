using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NoteConverter : MonoBehaviour
{
    [SerializeField] TextAsset noteData;
    private List<double> noteTimes;
    private double tolerance = 0.0001f;
    // Start is called before the first frame update
    void Awake()
    {
        LoadNoteTime();
    }

    void LoadNoteTime()
    {
        noteTimes = new List<double>();
        string[] lines = noteData.text.Split("\n");
        foreach (string line in lines)
        {
            string[] parts = line.Split(new char[] { '\t', ' ' }, System.StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length > 0 && float.TryParse(parts[0], out float time))
            {
                noteTimes.Add(time);
            }
        }
    }

    private void Update()
    {
        foreach (float noteTime in noteTimes)
        {
            if (IsApproximately(AudioSettings.dspTime, noteTime, tolerance))
            {
                Debug.Log($"{noteTime} beat!");
            }
        }
    }

    bool IsApproximately(double a, double b, double tolerance)
    {
        return System.Math.Abs(a - b) < tolerance;
    }
}
