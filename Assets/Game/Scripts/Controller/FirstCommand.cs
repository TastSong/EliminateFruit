using UnityEngine;
using System.Collections;
using strange.extensions.command.impl;
using UnityEngine.SceneManagement;

public class FirstCommand : EventCommand
{

    public override void Execute()
    {
        // 播放背景音乐
        SoundManager.Instance.PlayBGM("music");
        
        //设置一个游戏物体全局存在
        Object.DontDestroyOnLoad(GameObject.Find("Game"));

        //获取数据
        dispatcher.Dispatch(GameEvents.CommmandEvent.REQUEST_MAPINFO);

        //进入游戏
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

}
