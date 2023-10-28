using System.Collections.Generic;
using NewScripts.UIScripts;
using UnityEngine;

namespace NewScripts.StateMachine
{
    public class StartLoadState : IState<GameContext>
    {
        private readonly GameSetings _gameSettings;
        private readonly GraphPresenter _graphPresenter;

        private StateMachine<GameContext> _stateMachine;
        private GameContext _gameContext;

        private List<Vector2> _coordinatesPoints = new();
        private List<Color> _listColors = new();
        private List<int> _initialPointLocation = new();
        private List<Vector2> _connectionsBetweenPointsPairs = new();
        private int[,] _chipsArray;
        private List<int> _finishPointLocation = new();


        private int _index = -1;

        public StartLoadState(GameSetings gameSetings, GraphPresenter graphPresenter)
        {
            _gameSettings = gameSetings;
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
            FillFields();
            _graphPresenter.ShowBoardsEnded += TransitionAnotherState;
            _graphPresenter.Initialize(_coordinatesPoints,
                _connectionsBetweenPointsPairs,
                _initialPointLocation,
                _listColors,
                _finishPointLocation);
        }

        public void OnExit()
        {
            _graphPresenter.ShowBoardsEnded -= TransitionAnotherState;
        }

        private void TransitionAnotherState()
        {
            _stateMachine.Enter<SelectFirstNodeState>();
        }

        private void FillFields()
        {
            _coordinatesPoints = _gameSettings.ScriptableSettings[_index].CoordinatesPoints;
            _listColors = _gameSettings.ScriptableSettings[_index].ColorsChips;
            _initialPointLocation = _gameSettings.ScriptableSettings[_index].InitialPointLocation;
            _finishPointLocation = _gameSettings.ScriptableSettings[_index].FinishPointLocation;
            _connectionsBetweenPointsPairs = _gameSettings.ScriptableSettings[_index].ConnectionsBetweenPointPairs;
        }
    }
}