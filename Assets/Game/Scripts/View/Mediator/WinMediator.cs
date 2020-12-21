using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using UnityEngine.SceneManagement;

public class WinMediator : EventMediator
{
    [Inject]
    public WinView WinView { get; set; }

    public override void OnRegister()
    {
        WinView.btn_Menu.onClick.AddListener(onMenuClick);
        WinView.btn_Retry.onClick.AddListener(onRetryClick);
        WinView.btn_Next.onClick.AddListener(onNextClick);
        dispatcher.AddListener(GameEvents.ViewEvent.WINGAME, onWinGame);
    }

    public new void OnRemove()
    {
        WinView.btn_Menu.onClick.RemoveListener(onMenuClick);
        WinView.btn_Retry.onClick.RemoveListener(onRetryClick);
        WinView.btn_Next.onClick.RemoveListener(onNextClick);
        dispatcher.RemoveListener(GameEvents.ViewEvent.WINGAME, onWinGame);
    }

    void OnDestroy() { OnRemove(); }


    private void onWinGame(IEvent evt)
    {
        GameOverShowArgs e = evt.data as GameOverShowArgs;
        if (WinView == null)
        {
            dispatcher.RemoveListener(GameEvents.ViewEvent.WINGAME, onWinGame);
            return;
        }
        WinView.Show(e.CurrentScore, e.TargetScore);
    }


    private void onMenuClick()
    {
        SceneManager.LoadScene(1);
    }

    private void onRetryClick()
    {
        dispatcher.Dispatch(GameEvents.CommmandEvent.START_GAME, -1);
    }

    private void onNextClick()
    {
        dispatcher.Dispatch(GameEvents.CommmandEvent.START_GAME, -2);
    }
}
