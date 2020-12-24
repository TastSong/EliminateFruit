using UnityEngine;
using System.Collections.Generic;
using strange.extensions.mediation.impl;
using strange.extensions.dispatcher.eventdispatcher.api;
using strange.extensions.context.api;

public class TouchView : View
{
    [Inject(ContextKeys.CONTEXT_DISPATCHER)]
    public IEventDispatcher dispatcher { get; set; }

    private GameObject prevFruit;
    private int prevID;
    private bool needMusic;

    public bool Stop = false;
    public bool isSpeedUp = false;

    private List<GameObject> selectedFruits = new List<GameObject>();
    private List<GameObject> lines = new List<GameObject>();

    void Update()
    {
        if (Stop)
            return;

        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("按钮抬起");
            Time.timeScale = 1f;

            Collider2D col = Physics2D.OverlapPoint(point);
            if (col == null || !col.name.Contains("Fruit"))
                return;
            else
            {
                GameObject tempFruit = col.gameObject;
                tempFruit.GetComponent<FruitView>().Selected = true;

                prevFruit = tempFruit;
                prevID = tempFruit.GetComponent<FruitView>().FruitID;
                selectedFruits.Add(prevFruit);
                needMusic = true;
            }
        }

        if (Input.GetMouseButton(0))
        {
            Collider2D col = Physics2D.OverlapPoint(point);
            if (col == null || !col.name.Contains("Fruit"))
                return;
            else
            {
                if (needMusic)
                {
                    SoundManager.Instance.PlayAudio("selected");
                    needMusic = false;
                }
                GameObject tempFruit = col.gameObject;
                //判断是否拖拽到原来的水果上
                if (prevFruit != tempFruit)
                {

                    int tempID = tempFruit.GetComponent<FruitView>().FruitID;
                    //判断是否是相同的水果
                    if (tempID == prevID)
                    {
                        //判断距离
                        if (CheckDistance(tempFruit, prevFruit))
                        {
                            //要取消上次选择的水果
                            if (selectedFruits.Contains(tempFruit))
                            {
                                //选择的水果数量大于2，并且当前选中的水果是倒数第二个
                                if (selectedFruits.Count > 1 && selectedFruits[selectedFruits.Count - 2] == tempFruit)
                                {
                                    //删除线
                                    PoolManager.Instance.HideObjet(lines[lines.Count - 1]);
                                    lines.Remove(lines[lines.Count - 1]);
                                    //取消选择水果
                                    GameObject fruit = selectedFruits[selectedFruits.Count - 1];
                                    fruit.GetComponent<FruitView>().Selected = false;
                                    selectedFruits.Remove(fruit);
                                    prevFruit = tempFruit;
                                    needMusic = false;
                                }
                            }
                            //拖拽到新水果上
                            else
                            {
                                CreateLine(tempFruit, prevFruit);
                                prevFruit = tempFruit;
                                prevID = prevFruit.GetComponent<FruitView>().FruitID;
                                prevFruit.GetComponent<FruitView>().Selected = true;
                                selectedFruits.Add(prevFruit);
                                needMusic = true;
                            }
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (selectedFruits.Count > 1)
            {
                //发送一个事件，来删除
                dispatcher.Dispatch(GameEvents.CommmandEvent.DESTROY_FRUIT, selectedFruits);
                Debug.Log("按钮抬起");
                if (isSpeedUp) {
                    Time.timeScale = 1.6f;
                }
            }
            else if (selectedFruits.Count == 1)
            {
                GameObject fruit = selectedFruits[0];
                fruit.GetComponent<FruitView>().Selected = false;
            }
            selectedFruits.Clear();


            //删除线
            PoolManager.Instance.HideAllObject("Line");
            lines.Clear();
        }
    }


    private bool CheckDistance(GameObject tempFruit, GameObject prevFruit)
    {
        Vector2 tempPos = tempFruit.GetComponent<FruitView>().MapPosition;
        Vector2 prevPos = prevFruit.GetComponent<FruitView>().MapPosition;

        return Mathf.Abs(tempPos.x - prevPos.x) <= 1 && Mathf.Abs(tempPos.y - prevPos.y) <= 1;
    }


    private void CreateLine(GameObject tempFruit, GameObject prevFruit)
    {
        Vector2 linePos = new Vector2(
            (tempFruit.transform.position.x + prevFruit.transform.position.x) / 2,
            (tempFruit.transform.position.y + prevFruit.transform.position.y) / 2);

        Quaternion rotation = Quaternion.Euler(0, 0, GetRotationZ(tempFruit, prevFruit));

        GameObject go = PoolManager.Instance.GetObject("Line");
        go.transform.position = linePos;
        go.transform.rotation = rotation;

        lines.Add(go);
    }

    private float GetRotationZ(GameObject tempFruit, GameObject prevFruit)
    {
        Vector2 fruit1PosMap = tempFruit.GetComponent<FruitView>().MapPosition;
        Vector2 fruit2PosMap = prevFruit.GetComponent<FruitView>().MapPosition;

        if (fruit1PosMap.x == fruit2PosMap.x)
            return 0;
        if (fruit1PosMap.y == fruit2PosMap.y)
            return 90f;

        float temp = (fruit1PosMap.x - fruit2PosMap.x) * (fruit1PosMap.y - fruit2PosMap.y);

        if (temp < 0)
            return 45f;
        else
            return -45f;
    }

}
