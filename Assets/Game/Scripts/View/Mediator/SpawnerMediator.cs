using UnityEngine;
using System.Collections;
using strange.extensions.mediation.impl;
using strange.extensions.dispatcher.eventdispatcher.api;

public class SpawnerMediator : EventMediator
{
    [Inject]
    public SpawnerView SpawnerView { get; set; }

    public override void OnRegister()
    {
        SpawnerView.dispatcher.AddListener(GameEvents.ViewEvent.CREATEFRUIT, onViewInitFruit);

        dispatcher.AddListener(GameEvents.ViewEvent.NEWFRUIT, onNewFruit);

        SpawnerView.InitFruit();
    }

    public new void OnRemove()
    {
        SpawnerView.dispatcher.RemoveListener(GameEvents.ViewEvent.CREATEFRUIT, onViewInitFruit);
        dispatcher.RemoveListener(GameEvents.ViewEvent.NEWFRUIT, onNewFruit);
    }

    void OnDestroy() { OnRemove(); }


    private void onViewInitFruit(IEvent evt)
    {
        dispatcher.Dispatch(GameEvents.CommmandEvent.CREATE_FRUIT, evt.data);
    }

    private void onNewFruit(IEvent evt)
    {
        NewFruitArgs e = evt.data as NewFruitArgs;

        SpawnerView.NewFruit(e.XList, e.YList);
    }
}
