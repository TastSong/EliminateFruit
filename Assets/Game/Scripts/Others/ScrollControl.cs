using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;

public class ScrollControl : MonoBehaviour, IEndDragHandler
{
    public ScrollRect ScrollRect;

    private int page = 1;

    public int Page
    {
        get { return page; }

        set
        {
            this.page = Mathf.Clamp(value, 1, totalPage);
            target = (page - 1) * pageSize;
            start = true;
        }
    }

    private float target;
    private bool start;
    private int totalPage;
    private float pageSize;

    private void Start() {
        MapInfoList mapInfoList = JsonUtility.FromJson<MapInfoList>(Resources.Load<TextAsset>("Map").text);
        totalPage = (int)Mathf.Ceil(mapInfoList.MapList.Count / 9f);
        pageSize = 1f / (totalPage - 1);
        Debug.Log("++++++++++++totalPage " + totalPage);
        Debug.Log("++++++++++++pageSize " + pageSize);
    }

    void Update()
    {
        if (start)
        {
            if (Mathf.Abs(ScrollRect.horizontalNormalizedPosition - target) > 0.001f)
            {
                ScrollRect.horizontalNormalizedPosition =
                    Mathf.Lerp(ScrollRect.horizontalNormalizedPosition, target,
                    Time.deltaTime * 4f);
            }
            else
            {
                start = false;
            }
        }
    }

    public void ChangePage(int amount)
    {
        Page += amount;
        Debug.Log("++++++++++Page " + Page);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float h = ScrollRect.horizontalNormalizedPosition;
        Debug.Log("++ horizontalNormalizedPosition = " + h);
        for (int i = 0; i < totalPage; i++) {
            if (h > (i - 1) * pageSize + pageSize / 2 && h <= i * pageSize + pageSize / 2) {
                page = i + 1;
            }
        }
    }
}
