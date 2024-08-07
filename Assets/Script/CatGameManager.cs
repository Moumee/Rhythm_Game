using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatGameManager : MonoBehaviour
{
    [SerializeField] FishManager fishManager;
    PauseMenu pauseMenu;
    public int count = 0;
    private int lastFishMoveCount = -1;

    private int fishMoveCount = 0;

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

    private void Update()
    {
        if (!pauseMenu.isPlaying)
            return;

        

    }

    private void IterateChart()
    {
        if (!pauseMenu.isPlaying)
            return;
        

        

        
    }
}
