using strange.extensions.command.impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameOverCommand : EventCommand
{
    [Inject]
    public GameModel GameModel { get; set; }

    public override void Execute()
    {
        bool isWin = GameModel.CurrentScore >= GameModel.CurrentTarget;

        GameOverShowArgs e = new GameOverShowArgs()
        {
            CurrentScore = GameModel.CurrentScore,
            TargetScore = GameModel.CurrentTarget
        };


        if (isWin)
        {
            //赢了
            if (GameModel.CurrentLevel == GameModel.ReachedLevel)
                GameModel.ReachedLevel++;

            dispatcher.Dispatch(GameEvents.ViewEvent.WINGAME, e);
        }
        else
        {
            //输了
            dispatcher.Dispatch(GameEvents.ViewEvent.LOSEGAME, e);
        }
    }
}
