using strange.extensions.context.api;
using System;
using System.Collections;
using UnityEngine;

public class GameService : ISomeService
{
    public string Name { get; set; }

    [Inject(ContextKeys.CONTEXT_VIEW)]
    public GameObject ContextView { get; set; }

    public void AddListener(Action<string> listener)
    {

        TextAsset ta = Resources.Load<TextAsset>(Name);
        if (listener != null)
            listener(ta.text);
    }
}

