using UnityEngine;
using System.Collections;
using strange.extensions.context.impl;




public class Game : ContextView
{
    void Awake()
    {
        context = new GameContext(this, true);
        context.Start();
    }
}
