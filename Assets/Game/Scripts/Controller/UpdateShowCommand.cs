using System;
using System.Collections.Generic;
using strange.extensions.command.impl;
using UnityEngine.SceneManagement;

public class UpdateShowCommand : EventCommand
{
    [Inject]
    public GameModel GameModel { get; set; }


    public override void Execute()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.buildIndex == 2)
        {
            int reachedLevel = GameModel.ReachedLevel;
            dispatcher.Dispatch(GameEvents.ViewEvent.UPDATESHOW, reachedLevel);
        }
        else if (currentScene.buildIndex == 3)
        {
            UpdateShowArgs e = new UpdateShowArgs(GameModel.CurrentLevel, GameModel.CurrentTarget, GameModel.CurrentScore)
            {
                Time = GameModel.CurrentTime
            };
            dispatcher.Dispatch(GameEvents.ViewEvent.UPDATESHOW, e);
        }
    }
}
