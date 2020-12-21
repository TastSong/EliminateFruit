using UnityEngine;
using System.Collections;
using System;

public class ExplodeControl : ReuseableObject
{

    public override void BeforeGetObject()
    {
        StartCoroutine("Hide");
    }

    IEnumerator Hide()
    {
        yield return new WaitForSeconds(0.5f);

        PoolManager.Instance.HideObjet(gameObject);
    }

    public override void BeforeHideObject()
    {

    }
}
