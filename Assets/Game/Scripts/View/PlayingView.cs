using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using strange.extensions.mediation.impl;

public class PlayingView : EventView
{
    public Text txt_Score;
    public Text txt_Target;
    public Text txt_Level;
    public Slider sld_Timer;
    public Button stopBtn;
    public Sprite[] stopBtnSprites;
    public Button speedUpBtn;
    public Sprite[] speedUpSprites;

    private bool isStop = false;
    private bool isSpeedUp = false;

    protected override void Start() {
        base.Start();

        stopBtn.onClick.AddListener(() => {
            isStop = !isStop;
            GameObject.FindObjectOfType<TouchView>().Stop = isStop;
            stopBtn.GetComponent<Image>().sprite = isStop ? stopBtnSprites[0] : stopBtnSprites[1];
        });

        speedUpBtn.onClick.AddListener(() => {
            isSpeedUp = !isSpeedUp;
            GameObject.FindObjectOfType<TouchView>().isSpeedUp = isSpeedUp;
            speedUpBtn.GetComponent<Image>().sprite = isSpeedUp ? speedUpSprites[1] : speedUpSprites[0];
        });
    }

    public void UpdateShow(int lv, int score, int target)
    {
        this.txt_Level.text = lv.ToString();
        this.txt_Score.text = score.ToString();
        this.txt_Target.text = target.ToString();
    }

    public void ChangeScore(int score)
    {
        this.txt_Score.text = score.ToString();
    }

    public IEnumerator StartGame(float currentTime)
    {
        float timer = 0;
        while (timer < currentTime)//60
        {
            yield return new WaitForSeconds(1f);
            if (!isStop)
            {
                timer++;
                sld_Timer.value -= 1 / currentTime;
            }
        }

        GameObject.FindObjectOfType<TouchView>().Stop = true;

        //发送游戏结束的事件
        dispatcher.Dispatch(GameEvents.ViewEvent.GAMEOVER);
    } 
}
