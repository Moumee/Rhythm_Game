using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CatGameManager_2 : MonoBehaviour
{
    [SerializeField] FishManager fishManager;
    PauseMenu pauseMenu;
    public int count = 0;
    private int lastFishMoveCount = -1;

    public UnityEvent beat;

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
        Debug.Log("f2");
        //beat.Invoke();
        //if (!pauseMenu.isPlaying)
        //    return;
        //if (count >= 144)
        //{
        //    if (musicChart[count] == 1)
        //    {
        //        fishMoveCount++;

        //        if (fishMoveCount % 5 == 0 && fishMoveCount != lastFishMoveCount)
        //        {
        //            fishManager.MoveAllFish();

        //            lastFishMoveCount = fishMoveCount;
        //        }
        //    }



        //}

        //count++;

        
    }
}
