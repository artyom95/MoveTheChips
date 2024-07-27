using Cysharp.Threading.Tasks;
using NewScripts.Events;
using NewScripts.Extensions;
using NewScripts.Presenters;
using UniTaskPubSub;

namespace NewScripts.StateMachine
{
    public class StartLoadState : IState<GameContext>
    {
        private readonly GameSettings _gameSettings;
        private readonly GraphPresenter _graphPresenter;

        private StateMachine<GameContext> _stateMachine;
        private GameContext _gameContext;

        private int _index;
        private CompositeDisposable _subscriptions;
        private readonly AttemptsPresenter _attemptsPresenter;
        private readonly IAsyncSubscriber _subscriber;
        private readonly ButtonPresenter _buttonPresenter;

        public StartLoadState(GameSettings gameSettings,
            GraphPresenter graphPresenter,
            IAsyncSubscriber subscriber,
            AttemptsPresenter attemptsPresenter,
            ButtonPresenter buttonPresenter)
        {
            _buttonPresenter = buttonPresenter;
            _subscriber = subscriber;
            _attemptsPresenter = attemptsPresenter;
            _gameSettings = gameSettings;
            _graphPresenter = graphPresenter;
        }

        public void Initialize(StateMachine<GameContext> stateMachine, GameContext gameContext)
        {
            _stateMachine = stateMachine;
            _gameContext = gameContext;
        }

        public UniTask OnEnter()
        {
            _index = _gameContext.CurrentLoadStageIndex;
            //_index = 1;
            _subscriptions = new CompositeDisposable()
            {
                _subscriber.Subscribe<ShowBoardEvent>(OnShowBoardHandler),
                _subscriber.Subscribe<FindFinishPointsLocationEvent>(OnFindFinishPointLocationEventHandler),
                _subscriber.Subscribe<FindNodeModelsListEvent>(OnFindNodeModelListHandler),
            };

            Initialize();

            return UniTask.CompletedTask;
        }

        private async void Initialize()
        {
            var coordinatesPoints = _gameSettings.ScriptableSettings[_index].CoordinatesPoints;
            var listColors = _gameSettings.ScriptableSettings[_index].ColorsChips;
            var initialPointLocation = _gameSettings.ScriptableSettings[_index].InitialPointLocation;
            var finishPointLocation = _gameSettings.ScriptableSettings[_index].FinishPointLocation;
            var connectionsBetweenPointsPairs = _gameSettings.ScriptableSettings[_index].ConnectionsBetweenPointPairs;

            await _graphPresenter.Initialize(coordinatesPoints,
                connectionsBetweenPointsPairs,
                initialPointLocation,
                listColors,
                finishPointLocation);

            _buttonPresenter.Initialize(LoadNextStage, LoadCurrentStage);
            _attemptsPresenter.Initialize(_gameSettings.ScriptableSettings[_index].AmountAttempts, SetGameOverFlag);
        }

        public void OnExit()
        {
            _subscriptions.Dispose();
        }

        private void OnShowBoardHandler(ShowBoardEvent eventData)
        {
            _stateMachine.Enter<SelectFirstNodeState>();
        }

        private void OnFindFinishPointLocationEventHandler(FindFinishPointsLocationEvent eventData)
        {
            _gameContext.FinishPointLocation = eventData.FinishPointsLocation;
        }

        private void OnFindNodeModelListHandler(FindNodeModelsListEvent eventData)
        {
            _gameContext.NodeModelsList = eventData.NodeModelsList;
        }

        private void SetGameOverFlag()
        {
            _gameContext.IsGameOver = true;
        }

        private void LoadCurrentStage()
        {
            ResetSettings();
            _stateMachine.Enter<StartLoadState>();
        }

        private void LoadNextStage()
        {
            if (_gameContext.CurrentLoadStageIndex >= _gameSettings.ScriptableSettings.Count)
            {
                _gameContext.CurrentLoadStageIndex = _gameSettings.ScriptableSettings.Count - 1;
            }
            else if (_gameContext.CurrentLoadStageIndex == _gameSettings.ScriptableSettings.Count - 1)
            {
                _gameContext.CurrentLoadStageIndex = _gameContext.CurrentLoadStageIndex;
            }
            else
            {
                _gameContext.CurrentLoadStageIndex += 1;
            }

            ResetSettings();
            _stateMachine.Enter<StartLoadState>();
        }

        private void ResetSettings()
        {
            _graphPresenter.ClearGraph();
            _gameContext.IsGameOver = false;
            _attemptsPresenter.ResetAttempts();
        }
    }
}