using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataStorage
{
    public List<StageData> stages = new List<StageData>();
    

    public StageData Data_Hamster = new StageData(
       new List<int>
        {
            0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0,
            1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0,
            1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0,
            1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0,  
            1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0,
            0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            //1-2 start
            1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0,
            0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,
            0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0,
            0, 0, 1, 0, 0, 0, 1, 1, 0, 0, 1, 1, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0,
            1, 0, 0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 1, 0,
            0, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        },
        210, //BPM
        "HamsterHappy",
        "HamsterAngry",
        2,
        new List<int> { 135 }
    );
    public StageData Data_Cat = new StageData(
       new List<int>
        {
           0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,0,1,1,0,
           0,1,1,0,0,1,0,0,1,0,1,0,1,0,0,1,0,1,0,1,
           0,0,1,0,0,1,1,0,0,1,1,0,0,1,0,1,0,0,1,0,
           0,1,0,1,0,0,1,0,0,1,1,1,0,1,0,0,1,1,0,0,
           1,0,1,0,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,0,
           //(화면 전환 타이밍)
           1,0,0,1,0,0,0,1,1,0,0,1,0,0,0,1,0,0,1,0,
           1,0,1,0,0,1,0,1,0,1,0,0,1,0,1,0,0,1,1,1,
           0,1,0,0,1,1,0,0,1,0,1,0,0,0,0,1,0,0,0,0,
           1,1,1,1,0,0,0,0,0,1,1,1,1,0,0,0,0,1,0,0,
           1,0,0,1,0,1,1,0,0,0,1,0,1,1,0,0,1,0,0,1,
           0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0

        },
        283, //BPM
        "CatHappy",
        "CatAngry",
        2,
        new List<int> { 95 }
    );
    public StageData Data_Capybara = new StageData(
       new List<int>
        {
           0,0,0,0,1,0,0,1,0,0,0,1,0,1,0,1,0,1,0,0,
           0,1,0,0,0,1,0,1,0,1,0,0,0,1,1,1,1,0,0,1,
           1,0,0,1,1,0,0,1,0,1,0,0,1,0,1,0,0,1,0,1,
           1,1,0,1,0,0,1,0,0,1,0,1,0,0,1,0,0,1,0,1,
           0,1,0,1,0,1,1,1,0,1,0,1,1,0,0,0,0,0,0,0,
           0,0,0,0,0,0,0,0,0,0,0,
           //(꼬치 화면 전환 타이밍)
           1,1,0,0,1,0,0,1,0,1,0,1,1,1,0,1,0,0,0,1,
           1,0,0,1,0,0,1,0,1,0,0,1,0,1,0,1,0,1,1,0,
           0,1,1,1,1,0,0,0,1,0,1,0,0,1,0,1,0,0,0,1,  
           0,1,0,1,0,0,0,1,1,1,0,1,0,1,0,0,1,0,0,0,
           0,1,0,1,0,0,0,0,1,0,1,0,0,0,0,1,0,0,0,1,
           0,1,0,0,1,0,1,0,0,1,1,0,0,1,0,1,0,0,0,0,
           1,1,0,0,0,0,0,0,0,0,
           //(설탕 묻히기 화면 전환 타이밍)
           1,0,0,0,1,0,0,1,1,0,0,1,0,1,0,1,0,0,1,1,
           0,0,1,0,0,1,1,0,0,1,1,0,1,0,1,1,1,0,1,1,
           0,0,1,1,1,1,0,1,0,0,1,0,1,0,1,0,0,1,1,0,
           0,0,0,0,0,0,0,0,0,0,0,0,0

        },
        224, //BPM
        "CapybaraHappy",
        "CapybaraAngry",
        3,
        new List<int> { }
    );

    public StageData Data_Panda = new StageData(
       new List<int>
        {
           0,0,0,0,0,0,0,0,1,0,0,0,1,0,0,1,0,0,1,0,
           1,0,1,0,1,0,1,0,0,1,0,1,0,0,0,1,0,0,0,0,
           1,0,0,0,0,1,0,0,0,0,1,0,0,1,0,0,0,1,0,1,
           0,0,1,1,0,0,1,1,1,0,0,1,1,0,0,1,0,0,1,0,
           1,1,1,0,1,0,0,0,0,1,0,0,1,0,0,1,0,0,1,0,
           0,1,0,1,0,0,1,0,1,0,0,0,0,1,0,0,1,0,0,0,
           1,0,0,0,0,1,0,1,0,0,0,0,0,1,0,0,1,0,0,0,
           0,1,1,1,0,0,0,1,0,0,0,0,1,0,0,0,1,0,1,0,
           0,0,0,1,0,0,0,0,1,0,1,0,0,0,0,0,0,0,0,0,
           0,0,0,0,0,0,0,0,0,0,
           //(화면 전환 타이밍 / 죽순 쪼개기 => 양념 넣기)
           1,0,0,0,0,0,0,0,1,0,0,0,0,0,0,1,0,0,0,0,
           1,0,0,0,0,1,0,0,0,0,1,1,0,0,1,1,0,0,1,1,
           0,0,0,0,0,1,0,0,1,0,0,0,1,0,1,0,1,0,1,1,
           0,1,0,1,0,1,1,0,0,1,1,0,1,1,0,1,0,1,1,0,
           0,0,0,1,0,0,1,0,0,1,0,0,0,1,0,1,0,1,0,1,
           1,0,0,1,0,1,0,1,0,1,1,1,0,0,1,0,0,0,1,0,
           0,1,0,1,0,1,0,0,1,0,1,0,0,1,1,1,0,1,0,0,
           1,0,1,0,0,1,1,0,0,1,0,0,0,1,0,1,0,0,1,0,
           1,0,1,0,1,0,

        },
        330, //BPM
        "PandaHappy",
        "PandaAngry",
        3,
        new List<int> { }
    );

    public StageData Data_Lion = new StageData(
       new List<int>
        {
           0,0,0,0,0,0,0,0,0,0,0,1,0,0,1,0,0,1,0,0,
           1,0,0,1,0,0,1,1,1,0,1,0,1,0,1,1,1,1,0,1,
           0,0,1,0,1,0,0,1,0,1,0,1,1,0,1,0,1,0,1,1,
           0,1,0,0,1,0,1,0,1,0,1,1,0,1,0,1,0,1,1,0,
           1,0,1,0,1,1,0,1,0,0,0,0,0,0,0,0,0,0,0,
           //(양배추 -> 토마토 화면전환 타이밍)
           1,0,1,0,1,0,1,0,1,0,1,0,1,1,1,0,0,1,0,1,
           0,1,1,1,0,0,1,0,0,1,1,0,0,1,0,1,0,0,1,1,
           0,0,1,1,1,0,1,1,0,1,1,1,0,1,0,1,0,1,1,1,
           1,0,0,1,0,1,0,0,1,0,1,0,0,1,1,1,0,1,1,0,
           1,0,0,0,0,0,0,0,0,0,0,0,0,0,
           //(토마토 -> 버섯 화면전환 타이밍)
           1,0,0,0,1,0,0,1,0,0,1,0,0,1,1,1,1,0,1,0,
           1,1,1,1,0,1,0,0,1,1,0,1,0,0,1,1,0,1,1,0,
           1,0,1,1,1,0,1,0,1,0,1,1,1,0,1,0,1,0,0,1,
           1,0,1,1,1,1,0,1,0,0,1,0,1,1,0,1,1,1,1,0,
           0,0,0,0,0,0,
           //(버섯 -> 치즈 화면전환 타이밍)
           1,0,1,1,1,0,1,0,1,1,1,0,0,1,1,0,1,0,1,1,
           0,1,0,0,1,0,1,0,1,1,1,0,1,0,0,1,0,1,1,1,
           0,0,1,1,1,0,1,0,1,0,1,0,1,1,0,1,1,0,1,0,
           1,1,0,0,1,1,1,1,0,1,0,0,1,1,1,1,0,0,1,0,
           0,0,0,0,0,0,0,0,0,0,0,0
        },
        330, //BPM
        "LionHappy",
        "LionAngry",
        4,
        new List<int> { }
    );
}
