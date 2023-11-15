using System;
using System.Collections;
using System.Collections.Generic;
using NewScripts;
using NewScripts.Events;
using NewScripts.StateMachine;
using NewScripts.UIScripts;
using UniTaskPubSub;
using UnityEngine;


public class StartLoadState : IState<GameContext>
{
    private readonly GameSettings _gameSettings;
    private readonly GraphPresenter _graphPresenter;

    private StateMachine<GameContext> _stateMachine;
    private GameContext _gameContext;

    private int _index = -1;
    private readonly AsyncMessageBus _messageBus;
    private readonly List<IDisposable> _subscriptions = new();

    public StartLoadState(GameSettings gameSettings,
        GraphPresenter graphPresenter,
        AsyncMessageBus messageBus)
    {
        _messageBus = messageBus;
        _gameSettings = gameSettings;
        _graphPresenter = graphPresenter;
    }

    public void Initialize(StateMachine<GameContext> stateMachine, GameContext gameContext)
    {
        _stateMachine = stateMachine;
        _gameContext = gameContext;
    }

    public void OnEnter()
    {
        _index++;
        var coordinatesPoints = _gameSettings.ScriptableSettings[_index].CoordinatesPoints;
        var listColors = _gameSettings.ScriptableSettings[_index].ColorsChips;
        var initialPointLocation = _gameSettings.ScriptableSettings[_index].InitialPointLocation;
        var finishPointLocation = _gameSettings.ScriptableSettings[_index].FinishPointLocation;
        var connectionsBetweenPointsPairs = _gameSettings.ScriptableSettings[_index].ConnectionsBetweenPointPairs;

        _subscriptions.Add(_messageBus.Subscribe<ShowBoardEvent>(OnShowBoardHandler));

        _subscriptions.Add(_messageBus.Subscribe<FindFinishPointsLocationEvent>(OnFindFinishPointLocationEventHandler));
        _subscriptions.Add(_messageBus.Subscribe<FindNodeModelsList>(OnFindNodeModelListHandler));


        _graphPresenter.Initialize(coordinatesPoints,
            connectionsBetweenPointsPairs,
            initialPointLocation,
            listColors,
            finishPointLocation);
    }

    public void OnExit()
    {
        foreach (var subscription in _subscriptions)
        {
            subscription?.Dispose();
        }
    }

    private void OnShowBoardHandler(ShowBoardEvent eventData)
    {
        _stateMachine.Enter<SelectFirstNodeState>();
    }

    private void OnFindFinishPointLocationEventHandler(FindFinishPointsLocationEvent eventData)
    {
        _gameContext.FinishPointLocation = eventData.FinishPointsLocation;
    }

    private void OnFindNodeModelListHandler(FindNodeModelsList eventData)
    {
        _gameContext.NodeModelsList = eventData.NodeModelsList;
    }
}