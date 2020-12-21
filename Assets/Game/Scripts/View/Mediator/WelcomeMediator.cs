using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine.SceneManagement;

public class WelcomeMediator : Mediator
{
    [Inject]
    public WelcomeView WelcomeView { get; set; }

    //注册事件
    public override void OnRegister()
    {
        WelcomeView.btn_Play.onClick.AddListener(onPlayClick);
        WelcomeView.btn_Music.onClick.AddListener(onMusicClick);

        WelcomeView.SetImageState(SoundManager.Instance.Mute);
    }

    //移除事件
    public new void OnRemove()
    {
        WelcomeView.btn_Play.onClick.RemoveListener(onPlayClick);
        WelcomeView.btn_Music.onClick.RemoveListener(onMusicClick);
    }

    void OnDestroy()
    {
        OnRemove();
    }

    private void onPlayClick()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    private void onMusicClick()
    {
        SoundManager.Instance.Mute = !SoundManager.Instance.Mute;
        WelcomeView.SetImageState(SoundManager.Instance.Mute);
    }

}
