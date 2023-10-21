using System.Collections;
using System.Collections.Generic;
using NewScripts;
using NewScripts.StateMachine;
using Unity.VisualScripting;
using UnityEngine;

public class SelectSecondNodeState :  MonoBehaviour, IState <GameContext>
{
    [SerializeField] private NodeSelector _nodeSelector;
    [SerializeField] private PathFinder _pathFinder;
    [SerializeField] private SelectFirstNodeState _selectFirstNodeState;
    private StateMachine <GameContext> _stateMachine;
    private GameContext _gameContext;
  

    public void Initialize(StateMachine<GameContext> stateMachine, GameContext gameContext)
    {
        _gameContext = gameContext;
        _stateMachine = stateMachine;
    }

   

    public void OnEnter()
    {
        _nodeSelector.FirstNodeModelSelected += _selectFirstNodeState.SaveDataType;
        _nodeSelector.ChangeStateNodeSelector(2);
        _nodeSelector.SecondNodeModelSelected += SaveFinishNodeModel;
    }

    private void SaveFinishNodeModel(NodeModel finishNodeModel)
    {
        _gameContext.FinishNodeModel = finishNodeModel;
        FindPath();
    }

    private void FindPath()
    {
        
        var path =_pathFinder.FindMovingPath(_gameContext.NodeModelsList, _gameContext.StartNodeModel, _gameContext.FinishNodeModel);
      
        if (path.Count != 0)
        {
            _gameContext.Path = path;
            _stateMachine.Enter<ChipMovementState>();
        }
        else
        {
            _stateMachine.Enter<SelectFirstNodeState>();
        }
    }

    public void OnExit()
    {
        _nodeSelector.SecondNodeModelSelected -= SaveFinishNodeModel;
        _nodeSelector.FirstNodeModelSelected -= _selectFirstNodeState.SaveDataType;

    }
}
