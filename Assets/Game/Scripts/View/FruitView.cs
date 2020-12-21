using UnityEngine;
using System.Collections;
using System;

public class FruitView : ReuseableObject
{
    public Sprite[] FruitSprites;
    public Sprite[] PowerSprites;

    private SpriteRenderer[] spriteRenderers = null;
    public SpriteRenderer[] SpriteRenderers
    {
        get
        {
            return spriteRenderers;
        }
    }

    public int FruitID = -1;
    public Vector2 MapPosition;

    private Transform selectedTransform;

    public bool Selected
    {
        set
        {
            selectedTransform.gameObject.SetActive(value);
        }
    }

    private Transform powerTransform;
    private int power = 0;
    public int Power
    {
        get { return power; }
        set
        {
            if (value == 0 || value == power)
                return;

            SpriteRenderers[2].gameObject.SetActive(value >= 1 && value <= 3);
            SpriteRenderers[2].sprite = PowerSprites[value - 1];

            power = value;
        }
    }

    public void InitFruit(int fruitId, Vector2 pos)
    {
        SpriteRenderers[0].sprite = FruitSprites[fruitId];
        SpriteRenderers[1].sprite = FruitSprites[fruitId];
        this.FruitID = fruitId;
        this.MapPosition = pos;
        gameObject.name = "Fruit " + FruitID.ToString();
    }

    public override void BeforeGetObject()
    {
        if (spriteRenderers == null)
        {
            spriteRenderers = new SpriteRenderer[3];
            spriteRenderers[0] = GetComponent<SpriteRenderer>();
            spriteRenderers[1] = transform.GetChild(0).GetComponent<SpriteRenderer>();
            spriteRenderers[2] = transform.Find("Power").GetComponent<SpriteRenderer>();
        }

        if (selectedTransform == null)
            selectedTransform = transform.GetChild(0);

        Selected = false;
    }

    public override void BeforeHideObject()
    {
        power = 0;
        SpriteRenderers[2].gameObject.SetActive(false);

    }
}
