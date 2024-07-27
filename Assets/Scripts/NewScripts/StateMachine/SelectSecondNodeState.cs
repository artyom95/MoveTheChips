using Cysharp.Threading.Tasks;
using NewScripts.Node;
using UnityEngine;

namespace NewScripts.StateMachine
{
    public class SelectSecondNodeState : IState<GameContext>
    {
        private readonly NodeSelector _nodeSelector;
        private readonly PathFinder _pathFinder;
        private readonly SelectFirstNodeState _selectFirstNodeState;

        private StateMachine<GameContext> _stateMachine;
        private GameContext _gameContext;
        private readonly GameSettings _gameSettings;

        public SelectSecondNodeState(NodeSelector nodeSelector, PathFinder pathFinder,
            SelectFirstNodeState selectFirstNodeState,
            GameSettings gameSettings)
        {
            _gameSettings = gameSettings;
            _nodeSelector = nodeSelector;
            _pathFinder = pathFinder;
            _selectFirstNodeState = selectFirstNodeState;
        }

        public void Initialize(StateMachine<GameContext> stateMachine, GameContext gameContext)
        {
            _gameContext = gameContext;
            _stateMachine = stateMachine;
        }


        public UniTask OnEnter()
        {
            Debug.Log("Enter to the SelectSecondNodeState");
            _nodeSelector.FirstNodeModelSelected += _selectFirstNodeState.FillGameContext;
            _nodeSelector.SecondNodeModelSelected += SaveFinishNodeModel;
            _nodeSelector.SwitchedOfOutline += SwitchedOutline;
        
            return UniTask.CompletedTask;
        }

        private void SaveFinishNodeModel(NodeModel finishNodeModel)
        {
            _gameContext.FinishNodeModel = finishNodeModel;
            FindPath();
        }

        public void OnExit()
        {
            _nodeSelector.SecondNodeModelSelected -= SaveFinishNodeModel;
            _nodeSelector.FirstNodeModelSelected -= _selectFirstNodeState.FillGameContext;
            _nodeSelector.SwitchedOfOutline -= SwitchedOutline;
        }

        private void FindPath()
        {
            var indexSettings = _gameContext.CurrentLoadStageIndex;
            var path = _pathFinder.FindMovingPath(_gameSettings.ScriptableSettings[indexSettings].AmountColumnsForGraph,
                _gameContext.NodeModelsList, _gameContext.StartNodeModel,
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
        private void SwitchedOutline()
        {
            foreach (var nodeModel in _gameContext.HighlightingNodesList)
            {
                nodeModel.TurnOffOutline();
            }
            _gameContext.StartNodeModel.ChipModel.TurnOffOutline();
            _stateMachine.Enter<SelectFirstNodeState>();
        }
    }
}