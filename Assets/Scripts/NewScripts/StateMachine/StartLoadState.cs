using NewScripts.UIScripts;


namespace NewScripts.StateMachine
{
    public class StartLoadState : IState<GameContext>
    {
        private readonly GameSettings _gameSettings;
        private readonly GraphPresenter _graphPresenter;

        private StateMachine<GameContext> _stateMachine;
        private GameContext _gameContext;
        
        private int _index = -1;
        private NodePresenter _nodePresenter;

        public StartLoadState(GameSettings gameSettings, GraphPresenter graphPresenter, NodePresenter nodePresenter)
        {
            _nodePresenter = nodePresenter;
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

            
            _graphPresenter.ShowBoardsEnded += TransitionAnotherState;
            _graphPresenter.Initialize(coordinatesPoints,
                connectionsBetweenPointsPairs,
                initialPointLocation,
                listColors,
                finishPointLocation);
            
            _gameContext.FinishPointLocation = _nodePresenter.GetFinishPointLocation();
            var nodeModelList = _nodePresenter.GetNodeModelList();
            _gameContext.NodeModelsList = nodeModelList;
        }

        public void OnExit()
        {
            _graphPresenter.ShowBoardsEnded -= TransitionAnotherState;
        }

        private void TransitionAnotherState()
        {
            _stateMachine.Enter<SelectFirstNodeState>();
        }
        
    }
}