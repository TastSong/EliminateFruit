using System;
using System.Collections.Generic;

[Serializable]
public class MapInfo
{
    public int TargetScore;
    public float LimitTime;
    public int[] FruitIndexs = new int[5];
}

[Serializable]
public class MapInfoList
{
    public List<MapInfo> MapList;
}

