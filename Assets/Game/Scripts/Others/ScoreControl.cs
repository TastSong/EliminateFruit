using UnityEngine;
using System.Collections;
using System;

public class ScoreControl : ReuseableObject
{
    public Sprite[] ScoreSprites;

    private SpriteRenderer scoreRenderer;

    public void InitScore(int score)
    {
        if (score == -5)
            scoreRenderer.sprite = ScoreSprites[0];
        else if (score == 10)
            scoreRenderer.sprite = ScoreSprites[1];
        else if (score == 20)
            scoreRenderer.sprite = ScoreSprites[2];
        else if (score == 100)
            scoreRenderer.sprite = ScoreSprites[3];
        else if (score == 200)
            scoreRenderer.sprite = ScoreSprites[4];
    }

    public bool Special
    {
        set
        {
            if (value)
            {
                transform.localScale = new Vector3(2, 2);
            }
            else
            {
                transform.localScale = new Vector3(1, 1);
            }
        }
    }


    public override void BeforeGetObject()
    {
        if (scoreRenderer == null)
            scoreRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(Hide());
    }

    IEnumerator Hide()
    {
        float timer = 1f;
        while (timer > 0)
        {
            yield return 1f;
            timer -= Time.deltaTime;
            transform.Translate(Vector2.up * Time.deltaTime);
        }

        PoolManager.Instance.HideObjet(gameObject);
    }

    public override void BeforeHideObject()
    {
        Special = false;
    }
}
