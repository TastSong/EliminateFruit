using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class GameModel
{
    private List<MapInfo> mapList;
    public List<MapInfo> MapList
    {
        set
        {
            mapList = value;
        }     
    }

    //当前地图信息
    private MapInfo currentMapInfo;
    public int CurrentTarget { get { return currentMapInfo.TargetScore; } }
    public float CurrentTime { get { return currentMapInfo.LimitTime; } }
    public int[] CurrenFruits { get { return currentMapInfo.FruitIndexs; } }

    public int TotalPage {
        get {
            return (int)Mathf.Ceil(mapList.Count / 9f);
        }
    }

    //当前的关卡
    private int currentLevel;
    public int CurrentLevel
    {
        get { return currentLevel; }

        set
        {
            this.currentLevel = value;
            currentMapInfo = mapList[value - 1];
            CurrentScore = 0;
            Fruits = new List<GameObject>();
        }
    }

    //当前的分数
    private int currentScore;
    public int CurrentScore
    {
        get
        {
            return currentScore;
        }

        set
        {
            if (value < 0)
                currentScore = 0;
            else
                currentScore = value;
        }
    }

    //已经过关的最大关卡
    public int ReachedLevel
    {
        get { return PlayerPrefs.GetInt("ReachedLevel", 1); }
        set
        {
            value = Mathf.Clamp(value, 1, mapList.Count);
            PlayerPrefs.SetInt("ReachedLevel", value);
        }
    }

    //地图上所有水果
    public List<GameObject> Fruits;

}
