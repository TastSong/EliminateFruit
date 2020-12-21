using strange.extensions.command.impl;
using System.Collections.Generic;
using UnityEngine;


public class CreateFruitCommand : EventCommand
{
    [Inject]
    public GameModel GameModel { get; set; }

    public override void Execute()
    {
        CreateFruitArgs e = this.evt.data as CreateFruitArgs;

        GameObject fruit = PoolManager.Instance.GetObject("Fruit");
        fruit.transform.position = e.WorldPosition;
        fruit.transform.SetParent(e.Parent);

        //保存数据
        GameModel.Fruits.Add(fruit);

        //更改UI
        int fruitId = GameModel.CurrenFruits[Random.Range(0, 5)];
        fruit.GetComponent<FruitView>().InitFruit(fruitId, e.MapPosition);
    }
}
