using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TangerineFallManger : MonoBehaviour
{
    public Transform tangerineSpawnPoint;
    public TangerineFall[] tangerines;
    public Stick woodStick;
    private int index = 0;
    public int stickPressCount = 0;
    public Animator splashTopAnim;
    public Animator splashBottomAnim;
    private bool bottomSplashPlayed;
    private bool topSplashPlayed;
    
    void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tangerines[1].tangerineStuck && !bottomSplashPlayed)
        {
            splashBottomAnim.SetTrigger("BottomSplash");
            bottomSplashPlayed = true;
        }

        if (tangerines[2].tangerineStuck && !topSplashPlayed)
        {
            splashTopAnim.SetTrigger("TopSplash");
            topSplashPlayed = true;
        }
    }

    public void OnNoteHit()
    {
        if (!woodStick.isRotating)
        {
            tangerines[index].DropTangerine();
            index = (index + 1) % tangerines.Length;
            stickPressCount++;
            if (stickPressCount == tangerines.Length)
            {
                stickPressCount = 0;
                woodStick.RotateStick();
            }
        }
    }

    public void OnNoteMiss()
    {
        if (!woodStick.isRotating)
        {
            index = (index + 1) % tangerines.Length;
            stickPressCount++;
            if (stickPressCount == tangerines.Length)
            {
                stickPressCount = 0;
                woodStick.RotateStick();
            }
        }
        
    }

    public void ResetAllTangerines()
    {
        foreach (var tangerine in tangerines)
        {
            tangerine.transform.position = tangerineSpawnPoint.position;
            tangerine.tangerineStuck = false;
            tangerine.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        topSplashPlayed = false;
        bottomSplashPlayed = false;
    }
}
