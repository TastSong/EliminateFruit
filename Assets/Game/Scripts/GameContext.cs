using strange.extensions.context.api;
using strange.extensions.context.impl;
using System;
using UnityEngine;

public class GameContext : MVCSContext
{
    public GameContext(MonoBehaviour view, bool autoMapping)
        : base(view, autoMapping)
    {

    }

    //里面写各种绑定
    protected override void mapBindings()
    {
        //injection
        injectionBinder.Bind<ISomeService>().To<GameService>().ToSingleton();
        injectionBinder.Bind<GameModel>().To<GameModel>().ToSingleton();

        //commmand
        commandBinder.Bind(GameEvents.CommmandEvent.REQUEST_MAPINFO).To<RequestMapInfoCommand>();
        commandBinder.Bind(GameEvents.CommmandEvent.UPDATE_SHOW).To<UpdateShowCommand>();
        commandBinder.Bind(GameEvents.CommmandEvent.START_GAME).To<StartGameCommand>();
        commandBinder.Bind(GameEvents.CommmandEvent.GAME_OVER).To<GameOverCommand>();
        commandBinder.Bind(GameEvents.CommmandEvent.DESTROY_FRUIT).To<DestroyFruitCommand>();
        commandBinder.Bind(GameEvents.CommmandEvent.CREATE_FRUIT).To<CreateFruitCommand>();

        //mediator
        mediationBinder.Bind<WelcomeView>().To<WelcomeMediator>();
        mediationBinder.BindView<SelectMapView>().ToMediator<SelectMapMediator>();
        mediationBinder.Bind<PlayingView>().To<PlayingMediator>();
        mediationBinder.BindView<WinView>().ToMediator<WinMediator>();
        mediationBinder.Bind<LoseView>().ToMediator<LoseMediator>();
        mediationBinder.Bind<SpawnerView>().To<SpawnerMediator>();

        commandBinder.Bind(ContextEvent.START).To<FirstCommand>().Once();
    }

}

