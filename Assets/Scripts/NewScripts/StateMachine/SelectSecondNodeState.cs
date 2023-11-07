using NewScripts.Node;
using NewScripts.StateMachine;


public class SelectSecondNodeState : IState<GameContext>
{
    private readonly NodeSelector _nodeSelector;
    private readonly PathFinder _pathFinder;
    private readonly SelectFirstNodeState _selectFirstNodeState;

    private StateMachine<GameContext> _stateMachine;
    private GameContext _gameContext;

    public SelectSecondNodeState(NodeSelector nodeSelector, PathFinder pathFinder,
        SelectFirstNodeState selectFirstNodeState)
    {
        _nodeSelector = nodeSelector;
        _pathFinder = pathFinder;
        _selectFirstNodeState = selectFirstNodeState;
    }

    public void Initialize(StateMachine<GameContext> stateMachine, GameContext gameContext)
    {
        _gameContext = gameContext;
        _stateMachine = stateMachine;
    }


    public void OnEnter()
    {
        _nodeSelector.FirstNodeModelSelected += _selectFirstNodeState.SaveDataType;
        _nodeSelector.ChangeStateNodeSelector(typeStateSelector:2);
        _nodeSelector.SecondNodeModelSelected += SaveFinishNodeModel;
    }

    private void SaveFinishNodeModel(NodeModel finishNodeModel)
    {
        _gameContext.FinishNodeModel = finishNodeModel;
        FindPath();
    }

    public void OnExit()
    {
        _nodeSelector.SecondNodeModelSelected -= SaveFinishNodeModel;
        _nodeSelector.FirstNodeModelSelected -= _selectFirstNodeState.SaveDataType;
    }

    private void FindPath()
    {
        var path = _pathFinder.FindMovingPath(_gameContext.NodeModelsList, _gameContext.StartNodeModel,
            _gameContext.FinishNodeModel);

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
}