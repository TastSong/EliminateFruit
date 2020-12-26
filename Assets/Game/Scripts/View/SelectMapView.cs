using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using strange.extensions.mediation.impl;

public class SelectMapView : View
{
    public MapView[] MapViewArray;
    public Button btn_Back;
    
    public void Init()
    {
        MapViewArray = FindObjectsOfType<MapView>();       
    }

}
