using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine.SceneManagement;

public class LoseMediator : EventMediator
{
    [Inject]
    public LoseView LoseView { get; set; }

    public override void OnRegister()
    {
        LoseView.btn_Menu.onClick.AddListener(onMenuClick);
        LoseView.btn_ReStart.onClick.AddListener(onReStartClick);
        dispatcher.AddListener(GameEvents.ViewEvent.LOSEGAME, onLoseGame);
    }

    public new void OnRemove()
    {
        LoseView.btn_Menu.onClick.RemoveListener(onMenuClick);
        LoseView.btn_ReStart.onClick.RemoveListener(onReStartClick);
        dispatcher.RemoveListener(GameEvents.ViewEvent.LOSEGAME, onLoseGame);
    }

    void OnDestroy() { OnRemove(); }


    private void onLoseGame(IEvent evt)
    {
        GameOverShowArgs e = evt.data as GameOverShowArgs;
        if (LoseView == null)
        {
            dispatcher.RemoveListener(GameEvents.ViewEvent.WINGAME, onLoseGame);
            return;
        }
        LoseView.Show(e.CurrentScore, e.TargetScore);
    }


    private void onMenuClick()
    {
        SceneManager.LoadScene(1);
    }

    private void onReStartClick()
    {
        dispatcher.Dispatch(GameEvents.CommmandEvent.START_GAME, -1);
    }
}
