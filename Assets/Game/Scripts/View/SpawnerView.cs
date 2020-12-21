using UnityEngine;
using System.Collections.Generic;
using strange.extensions.mediation.impl;

public class SpawnerView : EventView
{
    public float[] FruitPosX =
      new float[] { -2.35f, -1.57f, -0.78f, 0f, 0.78f, 1.57f, 2.35f };
    public float FruitPosY = 6.0f;

    public void InitFruit()
    {
        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                CreateFruitArgs e = new CreateFruitArgs()
                {
                    MapPosition = new Vector2(i, j),
                    WorldPosition = new Vector2(FruitPosX[i], FruitPosY + j + 0.1f),
                    Parent = transform
                };

                dispatcher.Dispatch(GameEvents.ViewEvent.CREATEFRUIT, e);
            }
        }
    }


    /// <summary> 填充水果 </summary>
    /// <param name="xList">X的个数</param>
    /// <param name="yList">Y的个数</param>
    public void NewFruit(List<int> xList, List<int> yList)
    {
        int[] heigth = new int[7];
        for (int i = 0; i < xList.Count; i++)
        {
            int x = xList[i];
            int y = yList[i];

            heigth[x]++;

            CreateFruitArgs e = new CreateFruitArgs();
            e.MapPosition = new Vector2(x, y);
            e.WorldPosition = new Vector2(FruitPosX[x], 1.5f * FruitPosY - heigth[x]);
            e.Parent = this.transform;

            dispatcher.Dispatch(GameEvents.ViewEvent.CREATEFRUIT, e);

        }
    }

}

