using System;
using System.Collections.Generic;
using NewScripts;
using NewScripts.Node;
using NewScripts.StateMachine;
using NewScripts.UIScripts;
using UnityEditor.Experimental.GraphView;


public class SelectFirstNodeState : IState<GameContext>
{
    private readonly NodeSelector _nodeSelector;
    private readonly NodeView _nodeView;
    private readonly PathFinder _pathFinder;

    private StateMachine<GameContext> _stateMachine;
    private GameContext _gameContext;
    private int _amountMoves;


    public SelectFirstNodeState(NodeSelector nodeSelector, NodeView nodeView, PathFinder pathFinder)
    {
        _nodeSelector = nodeSelector;
        _nodeView = nodeView;
        _pathFinder = pathFinder;
    }

    public void Initialize(StateMachine<GameContext> stateMachine, GameContext gameContext)
    {
        _gameContext = gameContext;
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        _nodeSelector.FirstNodeModelSelected += SaveDataType;
        if (_amountMoves == 0)
        {
            _nodeSelector.ChangeStateNodeSelector(1);
            var nodeModelList = _nodeView.GetNodeModelList();
            SaveDataType(nodeModelList);
            _amountMoves++;
        }
        else
        {
            _nodeSelector.ChangeStateNodeSelector(1);
        }
    }

    public void SaveDataType<T>(T dataType)
    {
        if (dataType is NodeModel startNodeModel)
        {
            ResetHighlightNodes();
            _gameContext.StartNodeModel = startNodeModel;
            _gameContext.HighlightingNodesList = _pathFinder.FindHighlightingPath(_gameContext.StartNodeModel);
            HighlightNodes();
        }

        if (_gameContext.FinishPointLocation.Count == 0)
        {
            _gameContext.FinishPointLocation = _nodeView.FinishPointLocation;
        }

        if (dataType is List<NodeModel> list)
        {
            _gameContext.NodeModelsList = list;
            _gameContext.FinishIndexChips = new int [_gameContext.NodeModelsList.Count];
            FillFinishList();
        }
    }

    public void OnExit()
    {
        _nodeSelector.FirstNodeModelSelected -= SaveDataType;
    }

    private void FillFinishList()
    {
        for (var i = 0; i < _gameContext.NodeModelsList.Count; i++)
        {
            if (_gameContext.NodeModelsList[i].ChipModel == null)
            {
                _gameContext.FinishIndexChips[i] = 0;
            }
            else
            {
                _gameContext.FinishIndexChips[i] = -1;
            }
        }

        var number = 0;
        for (int i = 0; i < _gameContext.FinishIndexChips.Length; i++)
        {
            if (_gameContext.FinishIndexChips[i] != 0)
            {
                _gameContext.FinishIndexChips[i] = _gameContext.FinishPointLocation[number];
                number++;
            }
        }
    }

    private void HighlightNodes()
    {
        foreach (var nodeModel in _gameContext.HighlightingNodesList)
        {
            nodeModel.TurnOnOutline();
        }

        if (_gameContext.HighlightingNodesList.Count > 0)
        {
            _stateMachine.Enter<SelectSecondNodeState>();
        }
        else
        {
            _gameContext.StartNodeModel.ChipModel.TurnOffOutline();
            _stateMachine.Enter<SelectFirstNodeState>();
        }
    }

    private void ResetHighlightNodes()
    {
        foreach (var nodeModel in _gameContext.HighlightingNodesList)
        {
            nodeModel.TurnOffOutline();
        }
    }
}