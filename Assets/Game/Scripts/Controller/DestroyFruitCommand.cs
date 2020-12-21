using UnityEngine;
using System.Collections.Generic;
using strange.extensions.command.impl;
using System;

public class DestroyFruitCommand : EventCommand
{
    [Inject]
    public GameModel GameModel { get; set; }

    public override void Execute()
    {
        List<GameObject> fruits = evt.data as List<GameObject>;

        NewFruitArgs e = new NewFruitArgs();

        //得分
        int score = this.GetScore(fruits.Count);
        //得到奖励
        int power = this.GetBouns(fruits.Count);

        for (int i = 0; i < fruits.Count; i++)
        {
            //判断是否有神秘力量
            if (fruits[i].GetComponent<FruitView>().Power != 0)
            {
                int fruitPower = fruits[i].GetComponent<FruitView>().Power;
                Vector2 center = fruits[i].GetComponent<FruitView>().MapPosition;
                if (fruitPower == 1)
                    Power1Effect(center, fruits);
                else if (fruitPower == 2)
                    Power2Effect(center, fruits);
                else if (fruitPower == 3)
                    Power3Effect(center, fruits);
            }

            //得分 创建分数预设
            GameObject scoreGo = PoolManager.Instance.GetObject("Score");
            scoreGo.transform.position = fruits[i].transform.position;
            scoreGo.GetComponent<ScoreControl>().InitScore(score);
            GameModel.CurrentScore += score;

            Vector2 pos = fruits[i].GetComponent<FruitView>().MapPosition;

            /// <summary> 重置地图信息 </summary>
            ResetMap(pos, fruits[i]);

            //爆炸特效
            GameObject effectGo = PoolManager.Instance.GetObject("ExplodeEffect");
            effectGo.transform.position = fruits[i].transform.position;

            //创建新的水果
            int y = 0;
            foreach (var item in GameModel.Fruits)
            {
                if (item.GetComponent<FruitView>().MapPosition.x == pos.x)
                    y++;
            }
            e.YList.Add(y);
            e.XList.Add((int)pos.x);
        }
        //添加新水果
        dispatcher.Dispatch(GameEvents.ViewEvent.NEWFRUIT, e);

        //更新UI分数
        dispatcher.Dispatch(GameEvents.ViewEvent.CHANGESCORE, GameModel.CurrentScore);

        SoundManager.Instance.PlayAudio("clearmusic");


        System.Random r = new System.Random();
        int index = r.Next(0, fruits.Count);
        GameModel.Fruits[index].GetComponent<FruitView>().Power = power;

    }

    private void ResetMap(Vector2 pos, GameObject fruit)
    {
        GameModel.Fruits.Remove(fruit);

        for (int j = 0; j < GameModel.Fruits.Count; j++)
        {
            Vector2 mapPos = GameModel.Fruits[j].GetComponent<FruitView>().MapPosition;

            if (mapPos.x == pos.x && mapPos.y >= pos.y)
            {
                GameModel.Fruits[j].GetComponent<FruitView>().MapPosition.y -= 1;
            }
        }

        PoolManager.Instance.HideObjet(fruit);
    }

    private int GetScore(int fruitCount)
    {
        int score = 0;

        if (fruitCount < 3)
        {
            score = -5;
        }
        else if (fruitCount >= 3 && fruitCount < 6)
        {
            score = 10;
        }
        else if (fruitCount >= 6 && fruitCount < 8)
        {
            score = 10;

            GameObject scoreGo = PoolManager.Instance.GetObject("Score");
            scoreGo.transform.position = new Vector2();
            scoreGo.GetComponent<ScoreControl>().InitScore(100);
            scoreGo.GetComponent<ScoreControl>().Special = true;
            GameModel.CurrentScore += score;

            SoundManager.Instance.PlayAudio("goodmusic");
        }
        else if (fruitCount >= 8)
        {
            score = 20;

            GameObject scoreGo = PoolManager.Instance.GetObject("Score");
            scoreGo.transform.position = new Vector2();
            scoreGo.GetComponent<ScoreControl>().InitScore(200);
            scoreGo.GetComponent<ScoreControl>().Special = true;
            GameModel.CurrentScore += score;

            SoundManager.Instance.PlayAudio("goodmusic");
        }

        return score;
    }

    private int GetBouns(int fruitCount)
    {
        int power = 0;

        if (fruitCount > 5 && fruitCount <= 7)
        {
            power = 1;
            SoundManager.Instance.PlayAudio("props1music");
        }
        else if (fruitCount > 7 && fruitCount <= 9)
        {
            power = 2;
            SoundManager.Instance.PlayAudio("props2music");
        }
        else if (fruitCount > 9)
        {
            power = 3;
            SoundManager.Instance.PlayAudio("props3music");
        }
        return power;
    }


    /// <summary> 消除周围一圈的水果 </summary>
    private void Power1Effect(Vector2 center, List<GameObject> fruits)
    {
        foreach (GameObject fruit in GameModel.Fruits)
        {
            if (fruits.Contains(fruit) || fruit.GetComponent<FruitView>().Power != 0)
                continue;
            Vector2 pos = fruit.GetComponent<FruitView>().MapPosition;
            if (Mathf.Abs(pos.x - center.x) <= 1 && Mathf.Abs(pos.y - center.y) <= 1)
            {
                fruits.Add(fruit);
            }
        }
    }

    /// <summary> 消除一行或者一列的水果 </summary>
    private void Power2Effect(Vector2 center, List<GameObject> fruits)
    {
        int r = UnityEngine.Random.Range(0, 2);
        if (r == 0)
        {
            foreach (GameObject fruit in GameModel.Fruits)
            {
                if (fruits.Contains(fruit) || fruit.GetComponent<FruitView>().Power != 0)
                    continue;
                Vector2 pos = fruit.GetComponent<FruitView>().MapPosition;
                if (pos.x == center.x)
                {
                    fruits.Add(fruit);
                }
            }
        }
        else
        {
            foreach (GameObject fruit in GameModel.Fruits)
            {
                if (fruits.Contains(fruit) || fruit.GetComponent<FruitView>().Power != 0)
                    continue;
                Vector2 pos = fruit.GetComponent<FruitView>().MapPosition;
                if (pos.y == center.y)
                {
                    fruits.Add(fruit);
                }
            }
        }
    }

    /// <summary> 消除一行和一列的水果 </summary>
    private void Power3Effect(Vector2 center, List<GameObject> fruits)
    {
        foreach (GameObject fruit in GameModel.Fruits)
        {
            if (fruits.Contains(fruit) || fruit.GetComponent<FruitView>().Power != 0)
                continue;
            Vector2 pos = fruit.GetComponent<FruitView>().MapPosition;
            if (pos.x == center.x || pos.y == center.y)
            {
                fruits.Add(fruit);
            }
        }
    }

}
