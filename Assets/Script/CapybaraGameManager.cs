using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapybaraGameManager : MonoBehaviour
{
    [SerializeField] StickManager stickManager;
    PauseMenu pauseMenu;
    public int count = 0;
    private int stickMoveCount = 0;
    private int lastStickMoveCount = -1;

    private List<int> musicChart = new List<int>
    {
        0,0,0,0,1,0,0,0,1,0,0,0,0,1,0,1,0,1,0,1,0,0,0,0,1,0,0,0,0,1,0,1,0,1,0,0,1,0,
        0,0,0,1,1,1,1,0,0,1,1,0,0,1,1,0,0,1,0,1,0,0,0,1,0,0,1,0,1,0,0,0,1,0,1,1,1,0,
        1,0,0,1,0,0,1,0,1,0,0,0,0,1,0,1,0,0,1,0,0,0,1,0,0,0,1,0,1,0,1,0,1,1,1,0,0,1,
        0,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0, //next substage
        1,1,0,0,1,0,0,1,0,0,1,0,1,0,1,1,1,0,1,0,0,0,1,0,1,0,0,0,0,1,1,0,0,1,0,0,0,1,
        0,1,0,0,0,1,0,1,0,0,1,1,0,0,0,1,1,1,1,0,0,0,1,0,1,0,0,0,1,0,1,0,1,0,1,1,0,0,
        1,0,0,0,0,0,0,1,0,0,0,1,0,0,0,0,0,1,1,1,0,0,1,0,0,1,1,0,0,0,1,0,1,0,1,0,0,0,
        0,1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,0,1,0,0,1,0,1,0,0,0,0,1,1,0,0,0,1,0,0,0,0,0,
        1,0,0,0,0,0,0,0,0,0,0,0,0, // next substage
        1,0,0,0,1,0,0,0,1,0,0,1,1,0,0,0,1,0,1,0,1,0,0,1,1,0,0,0,1,0,0,0,1,0,0,0,1,1,
        0,0,1,0,0,0,1,0,1,0,1,0,1,1,1,0,1,0,1,0,1,1,1,0,0,0,0,1,1,1,1,0,0,1,0,0,1,0,
        1,0,0,0,1,0,0,1,0,1,1,0,1,0,1,0,1,0,0,0,1,0,1,0,0,0,1,0,1,0,0,1,1,0,0,0,0,0,
        0,0,0,0,0,0,0,0,0,0,0,0
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
        
        if (count > 129)
        {
            if (musicChart[count] == 1)
            {
                stickMoveCount++;
                if (stickMoveCount % 3 == 0 && stickMoveCount != lastStickMoveCount)
                {
                    stickManager.RotateStick();
                    lastStickMoveCount = stickMoveCount;
                }
            }
        }

        count++;


    }
}
