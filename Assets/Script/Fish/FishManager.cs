using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{
    private bool secondFishActive = false;
    private bool thirdFishActive = false;
    public List<Fish> fishList = new List<Fish>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void MoveAllFish()
    {
        foreach (Fish fish in fishList)
        {
            if (fish.gameObject.activeInHierarchy)
            {
                fish.MoveFish();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (fishList[0].positionId == 1 && !secondFishActive)
        {
            fishList[1].gameObject.SetActive(true);
            secondFishActive = true;
        }

        if (fishList[1].positionId == 1 && !thirdFishActive)
        {
            fishList[2].gameObject.SetActive(true);
            thirdFishActive = true;
        }

    }
}
