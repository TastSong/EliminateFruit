using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using strange.extensions.mediation.impl;

public class WelcomeView : View
{
    public Button btn_Music;
    public Image img_Music;
    public Button btn_Play;

    public Sprite[] MusicState;

    public void SetImageState(bool mute)
    {
        int index = mute ? 1 : 0;
        img_Music.sprite = MusicState[index];
    }

}

