using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using UnityEngine.SceneManagement;
using strange.extensions.dispatcher.eventdispatcher.api;

public class SelectMapMediator : EventMediator
{
    [Inject]
    public SelectMapView SelectMapView { get; set; }

    public override void OnRegister()
    {
        // View 初始化
        SelectMapView.Init();
        for (int i = 0; i < SelectMapView.MapViewArray.Length; i++) {
            SelectMapView.MapViewArray[i].OnClick += onMapClick;
            
        }
        Debug.Log("++SelectMapView.MapViewArray MapView " + SelectMapView.MapViewArray.Length);
        SelectMapView.btn_Back.onClick.AddListener(onBackClick);

        //绑定事件
        dispatcher.AddListener(GameEvents.ViewEvent.UPDATESHOW, onUpdateShow);

        // 请求数据
        dispatcher.Dispatch(GameEvents.CommmandEvent.UPDATE_SHOW);
    }


    public new void OnRemove()
    {
        SelectMapView.btn_Back.onClick.RemoveListener(onBackClick);
        dispatcher.RemoveListener(GameEvents.ViewEvent.UPDATESHOW, onUpdateShow);
    }

    void OnDestroy()
    {
        OnRemove();
    }

    private void onMapClick(int level)
    {
        //发一个命令，开始游戏，传递的参数是level
        dispatcher.Dispatch(GameEvents.CommmandEvent.START_GAME, level);
    }

    private void onBackClick()
    {
        SceneManager.LoadScene(1);
    }

    private void onUpdateShow(IEvent evt)
    {
        int reachedLevel = (int)evt.data;
        //3 4 123 4
        for (int i = 0; i < SelectMapView.MapViewArray.Length; i++)
        {
            bool locked = SelectMapView.MapViewArray[i].Level > reachedLevel;
            SelectMapView.MapViewArray[i].Locked = locked;
            if (SelectMapView.MapViewArray[i].Level < reachedLevel) {
                SelectMapView.MapViewArray[i].SetStar();
            }
        }
    }
}
