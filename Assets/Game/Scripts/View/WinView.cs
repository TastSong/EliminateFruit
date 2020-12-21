using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine.UI;

public class WinView : View
{
    public Text Score;
    public Text Target;

    public Button btn_Menu;
    public Button btn_Retry;
    public Button btn_Next;

    public void Show(int score, int target)
    {
        gameObject.SetActive(true);
        Score.text = score.ToString();
        Target.text = target.ToString();
    }
}
