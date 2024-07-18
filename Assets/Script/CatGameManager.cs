using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatGameManager : MonoBehaviour
{
    PauseMenu pauseMenu;
    public int count = 0;

    private List<int> musicChart = new List<int>
    {
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

    // Start is called before the first frame update
    void Awake()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
        BeatTracker.OnFixedBeat += IterateChart;
    }

    private void OnDestroy()
    {
        BeatTracker.OnFixedBeat -= IterateChart;
    }

    private void IterateChart()
    {
        if (!pauseMenu.isPlaying)
            return;

        if (musicChart[count + 2] == 1)
        {
            //spawn note
        }
        count++;

        
    }
}
