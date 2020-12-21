using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.dispatcher.eventdispatcher.api;

public class PlayingMediator : EventMediator
{
    [Inject]
    public PlayingView PlayingView { get; set; }

    public override void OnRegister()
    {
        PlayingView.dispatcher.AddListener(GameEvents.ViewEvent.GAMEOVER, onGameOver);

        dispatcher.AddListener(GameEvents.ViewEvent.UPDATESHOW, onUpdateShow);
        dispatcher.AddListener(GameEvents.ViewEvent.CHANGESCORE, onChangeScore);

        dispatcher.Dispatch(GameEvents.CommmandEvent.UPDATE_SHOW);
    }

    public new void OnRemove()
    {
        PlayingView.dispatcher.RemoveListener(GameEvents.ViewEvent.GAMEOVER, onGameOver);

        dispatcher.RemoveListener(GameEvents.ViewEvent.UPDATESHOW, onUpdateShow);

        dispatcher.RemoveListener(GameEvents.ViewEvent.CHANGESCORE, onChangeScore);
    }

    void OnDestroy()
    {
        OnRemove();
    }

    private void onUpdateShow(IEvent evt)
    {
        UpdateShowArgs e = evt.data as UpdateShowArgs;
        PlayingView.UpdateShow(e.Level, e.Score, e.Target);
        //开启倒计时
        StartCoroutine(PlayingView.StartGame(e.Time));
    }

    private void onGameOver()
    {
        //发起一个事件，创建一个命令，判断游戏结束
        dispatcher.Dispatch(GameEvents.CommmandEvent.GAME_OVER);
    }

    private void onChangeScore(IEvent evt)
    {
        int score = (int)evt.data;
        PlayingView.ChangeScore(score);
    }

}
