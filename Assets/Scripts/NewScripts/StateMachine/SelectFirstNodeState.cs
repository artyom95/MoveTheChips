using NewScripts.Node;
using NewScripts.StateMachine;
using NewScripts.UIScripts;


public class SelectFirstNodeState : IState<GameContext>
{
    private readonly NodeSelector _nodeSelector;
    private readonly NodeView _nodeView;
    private readonly PathFinder _pathFinder;

    private StateMachine<GameContext> _stateMachine;
    private GameContext _gameContext;


    public SelectFirstNodeState(NodeSelector nodeSelector, PathFinder pathFinder)
    {
        _nodeSelector = nodeSelector;
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

        _nodeSelector.ChangeStateNodeSelector(typeStateSelector: 1);
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