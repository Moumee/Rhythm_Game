using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NoteConverter : MonoBehaviour
{
    [SerializeField] TextAsset noteData;
    public List<float> noteTimes;
    void Awake()
    {
        LoadNoteTime();
    }

    void LoadNoteTime()
    {
        noteTimes = new List<float>();
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

    
}
