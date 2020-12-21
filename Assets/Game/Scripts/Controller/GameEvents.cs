using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class GameEvents
{
    public enum CommmandEvent
    {
        REQUEST_MAPINFO = 0,
        UPDATE_SHOW = 1,
        START_GAME = 2,
        GAME_OVER = 3,
        CREATE_FRUIT = 9,
        DESTROY_FRUIT
    }

    public enum ViewEvent
    {
        UPDATESHOW = 0,
        GAMEOVER = 1,
        WINGAME = 2,
        LOSEGAME = 3,
        CREATEFRUIT,
        CHANGESCORE,
        NEWFRUIT
    }

}

