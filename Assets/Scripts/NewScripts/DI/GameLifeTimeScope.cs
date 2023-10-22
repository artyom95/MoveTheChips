using System.Collections;
using System.Collections.Generic;
using NewScripts;
using NewScripts.StateMachine;
using NewScripts.UIScripts;
using OLDScripts;
using Unity.VisualScripting;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifeTimeScope : LifetimeScope
{
    [SerializeField] private ChipView _chipView;
    [SerializeField] private GraphView _graphView;
    [SerializeField] private NodeView _nodeView;
    [SerializeField] private GameSetings _gameSetings;
    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);
        RegisterGameStateMachine(builder);


        builder.RegisterEntryPoint<GameController>();

        builder.Register<ChipPresenter>(Lifetime.Singleton);
        builder.Register<GraphPresenter>(Lifetime.Singleton);
        builder.Register<NodePresenter>(Lifetime.Singleton);
        
        builder.Register<NodeModel>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<ChipModel>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<PathFinder>(Lifetime.Singleton);
            
        builder.RegisterInstance(_chipView);
        builder.RegisterInstance(_graphView);
        builder.RegisterInstance(_nodeView);
        builder.RegisterInstance(_gameSetings);

    }

    private static void RegisterGameStateMachine(IContainerBuilder builder)
    {
        builder.Register<StateMachine<GameContext>>(Lifetime.Singleton);

        builder.Register<StartLoadState>(Lifetime.Singleton);
        builder.Register<SelectFirstNodeState>(Lifetime.Singleton);
        builder.Register<SelectSecondNodeState>(Lifetime.Singleton);
        builder.Register<ChipMovementState>(Lifetime.Singleton);
        builder.Register<FinishGameState>(Lifetime.Singleton);

    }
}
