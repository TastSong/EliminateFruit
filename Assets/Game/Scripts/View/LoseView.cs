using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using strange.extensions.mediation.impl;

public class LoseView : View
{
    public Text Score;
    public Text Target;

    public Button btn_Menu;
    public Button btn_ReStart;

    public void Show(int score, int target)
    {
        gameObject.SetActive(true);
        Score.text = score.ToString();
        Target.text = target.ToString();
    }
}
