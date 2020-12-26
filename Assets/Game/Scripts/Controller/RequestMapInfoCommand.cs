using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using UnityEngine;


public class RequestMapInfoCommand : Command
{
    [Inject]
    public ISomeService GameService { get; set; }

    [Inject]
    public GameModel GameModel { get; set; }

    public override void Execute()
    {
        Retain();

        GameService.Name = "Map";
        GameService.AddListener(onComplete);
    }

    private void onComplete(string json)
    {
        Release();

        MapInfoList mapInfoList = JsonUtility.FromJson<MapInfoList>(json);
        GameModel.MapList = mapInfoList.MapList;
        Debug.Log("++++++++++++GameModel.TotalPage " + GameModel.TotalPage);
    }

}

