using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameCommand : EventCommand
{
    [Inject]
    public GameModel GameModel { get; set; }

    public override void Execute()
    {
        int level = (int)this.evt.data;

        if (level == -1)// -1 就是 继续本关
        { GameModel.CurrentLevel = GameModel.CurrentLevel; }
        else if (level == -2)// -2 就是 开始下关
        { GameModel.CurrentLevel++; }
        else
            GameModel.CurrentLevel = level;

        SceneManager.LoadScene("3.playing");
        PoolManager.Instance.InitAllPool();
    }
}
