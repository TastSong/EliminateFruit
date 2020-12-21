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
            this.page = Mathf.Clamp(value, 1, 3);
            if (page == 1)
            {
                target = 0;
            }
            else if (page == 2)
            {
                target = 0.5f;
            }
            else if (page == 3)
            {
                target = 1f;
            }
            start = true;
        }
    }

    private float target;
    private bool start;

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
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float h = ScrollRect.horizontalNormalizedPosition;

        if (h <= 0.25f)
        {
            Page = 1;
        }
        else if (h > 0.25f && h < 0.75f)
        {
            Page = 2;
        }
        else if (h >= 0.75f)
        {
            Page = 3;
        }
    }
}
