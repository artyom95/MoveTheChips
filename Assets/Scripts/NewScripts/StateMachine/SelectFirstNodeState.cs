using System;
using System.Collections.Generic;
using NewScripts;
using NewScripts.StateMachine;
using NewScripts.UIScripts;
using UnityEngine;

[Serializable]
public class SelectFirstNodeState : MonoBehaviour, IState<GameContext>
{
    [SerializeField] private NodeSelector _nodeSelector;
    [SerializeField] private NodeView _nodeView;
    [SerializeField] private PathFinder _pathFinder;
    private StateMachine<GameContext> _stateMachine;
    private GameContext _gameContext;
    private int _amountMoves  = 0;

    public void Initialize(StateMachine<GameContext> stateMachine, GameContext gameContext)
    {
        _gameContext = gameContext;
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
       // _nodeSelector.ChoseAnotherNodeModel += ResetHighlightNodes;
        _nodeSelector.FirstNodeModelSelected += SaveDataType;
        if (_amountMoves ==0)
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

        if (dataType is List<NodeModel> list)
        {
            _gameContext.NodeModelsList = list;
        }

        if (_gameContext.FinishPointLocation.Count ==0)
        {
            _gameContext.FinishPointLocation = _nodeView.FinishPointLocation;
            
        }
    }

    public void OnExit()
    {
       // _nodeSelector.ChoseAnotherNodeModel -= ResetHighlightNodes;
        _nodeSelector.FirstNodeModelSelected -= SaveDataType;
    }

    private void HighlightNodes()
    {
        foreach (var nodeModel in _gameContext.HighlightingNodesList)
        {
            nodeModel.TurnOnOutline();
        }

        if (_gameContext.HighlightingNodesList.Count>0)
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