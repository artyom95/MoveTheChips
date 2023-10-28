using System.Collections.Generic;
using NewScripts;
using NewScripts.StateMachine;
using NewScripts.UIScripts;



public class SelectFirstNodeState :  IState<GameContext>
{
    private readonly NodeSelector _nodeSelector;
    private readonly NodeView _nodeView;
    private readonly PathFinder _pathFinder;

    private StateMachine<GameContext> _stateMachine;
    private GameContext _gameContext;
    private int _amountMoves ;

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