using NewScripts;
using NewScripts.Chip;
using NewScripts.GameObjectsPresenter;
using NewScripts.Node;
using NewScripts.StateMachine;
using NewScripts.UIScripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameLifeTimeScope : LifetimeScope
{
    [SerializeField] private ChipView _chipView;
    [SerializeField] private GraphView _graphView;
    [SerializeField] private NodeView _nodeView;
    [SerializeField] private GameSettings _gameSetings;
    [SerializeField] private NodeSelector _nodeSelector;
    [SerializeField] private ChipMover _chipMover;

    [SerializeField] private GameObject _mainPanel;
    [SerializeField] private GameObject _secondPanel;

    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;

   

    protected override void Configure(IContainerBuilder builder)
    {
        base.Configure(builder);
        RegisterGameStateMachine(builder);

       
        builder.Register<ChipPresenter>(Lifetime.Singleton);
        builder.Register<GraphPresenter>(Lifetime.Singleton);
        builder.Register<NodePresenter>(Lifetime.Singleton);

        builder.Register<NodeModel>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<ChipModelSettings>(Lifetime.Singleton).AsImplementedInterfaces();
        builder.Register<PathFinder>(Lifetime.Singleton);

        builder.Register<GameOverController>(Lifetime.Singleton);

        builder.RegisterInstance(_chipView);
        builder.RegisterInstance(_graphView);
        builder.RegisterInstance(_nodeView);
        builder.RegisterInstance(_gameSetings);
      

        var panelPresenterFactory = new PanelPresenter(_mainPanel, _secondPanel, _winPanel, _losePanel);
        builder.RegisterInstance(panelPresenterFactory);
        
        builder.RegisterComponent(_nodeSelector);
        builder.RegisterComponent(_chipMover);
        builder.RegisterEntryPoint<GameController>();

    }


    private static void RegisterGameStateMachine(IContainerBuilder builder)
    {
        builder.Register<StartLoadState>(Lifetime.Singleton);
        builder.Register<SelectFirstNodeState>(Lifetime.Singleton);
        builder.Register<SelectSecondNodeState>(Lifetime.Singleton);
        builder.Register<ChipMovementState>(Lifetime.Singleton);
        builder.Register<FinishGameState>(Lifetime.Singleton);
    }
}