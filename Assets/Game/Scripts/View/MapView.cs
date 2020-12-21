using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System;
using UnityEngine.UI;

public class MapView : MonoBehaviour, IPointerClickHandler
{
    public Sprite[] States;

    public int Level = -1;

    private bool locked = true;
    public bool Locked
    {
        get { return locked; }
        set
        {
            if (!value)
                GetComponentInChildren<Text>().text = Level.ToString();
            int index = value ? 0 : 1;
            GetComponent<Image>().sprite = States[index];

            locked = value;
        }
    }

    public void SetStar()
    {
        GetComponent<Image>().sprite = States[2];
    }

    public Action<int> OnClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Locked && OnClick != null)
        {
            OnClick(this.Level);
        }
    }

    void OnDestroy()
    {
        while (OnClick != null)
            OnClick -= OnClick;
    }
}
