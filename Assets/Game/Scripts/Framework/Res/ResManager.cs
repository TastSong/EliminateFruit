using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class ResManager : MonoBehaviour
{
    private static ResManager instance;
    public static ResManager Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        instance = this;
    }

    //保存加载完成的资源
    private Dictionary<string, PrefabToLoad> loadedPrefabDict = new Dictionary<string, PrefabToLoad>();

    //加载资源
    public UnityEngine.Object LoadPrefab(string prefabName)
    {
        if (String.IsNullOrEmpty(prefabName))
            return null;
        else if (loadedPrefabDict.ContainsKey(prefabName))
        {
            return loadedPrefabDict[prefabName].Prefab;
        }
        else
        {
            PrefabToLoad prefab = new PrefabToLoad()
            {
                PrefabName = prefabName,
                Prefab = Resources.Load(prefabName)
            };
            loadedPrefabDict.Add(prefabName, prefab);
            return loadedPrefabDict[prefabName].Prefab;
        }
    }

    //卸载资源
    public void UnLoadPrefab(string prefabName)
    {
        if (loadedPrefabDict.ContainsKey(prefabName))
            return;
        else
        {
            Resources.UnloadAsset(loadedPrefabDict[prefabName].Prefab);
            loadedPrefabDict[prefabName] = null;
            loadedPrefabDict.Remove(prefabName);
        }
    }

    //卸载所有资源
    public void UnLoadAll()
    {
        foreach (KeyValuePair<string,PrefabToLoad> kv in loadedPrefabDict)
        {
            loadedPrefabDict[kv.Key] = null;
        }
        loadedPrefabDict.Clear();
        Resources.UnloadUnusedAssets();
    }

    // 需要加载的预设类
    public class PrefabToLoad
    {
        public string PrefabName;
        public UnityEngine.Object Prefab;
    }

}
