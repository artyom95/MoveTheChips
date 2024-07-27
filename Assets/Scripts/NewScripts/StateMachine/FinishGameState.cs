using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using NewScripts.Extensions;
using NewScripts.Node;
using NewScripts.Presenters;
using UnityEngine;

namespace NewScripts.StateMachine
{
    public class FinishGameState : IState<GameContext>
    {
        private StateMachine<GameContext> _stateMachine;
        private GameContext _gameContext;

        private bool _isFillFields;
        private CompositeDisposable _subscriptions;
        private readonly GameResultPanelPresenter _gameResultPanelPresenter;
        private bool _isGameOver;

        public FinishGameState(GameResultPanelPresenter gameResultPanelPresenter)
        {
            _gameResultPanelPresenter = gameResultPanelPresenter;
        }

        public void Initialize(StateMachine<GameContext> stateMachine, GameContext gameContext)
        {
            _gameContext = gameContext;
            _stateMachine = stateMachine;
        }

        public UniTask OnEnter()
        {
            var matchesArray = new bool[_gameContext.FinishPointLocation.Count];
            var nodeModelList = _gameContext.NodeModelsList;
            var finishPointLocation = _gameContext.FinishPointLocation;
            
            CheckOutMatches(finishPointLocation, nodeModelList, matchesArray);

            ShowResultPanel(matchesArray);

            return UniTask.CompletedTask;
        }
    
        private void ShowResultPanel(bool[] matchesArray)
        {
            var isFinish = IsFinishState(matchesArray);
            if (isFinish)
            {
                _gameResultPanelPresenter.ShowWinScreen();
            }
            else if (_gameContext.IsGameOver)
            {
                _gameResultPanelPresenter.ShowLoseScreen();
            }
            else
            {
                _stateMachine.Enter<SelectFirstNodeState>();
            }
        }

        private void CheckOutMatches(List<int> finishPointLocation, List<NodeModel> nodeModelList, bool[] matchesArray)
        {
            for (var i = 0; i < finishPointLocation.Count; i++)
            {
                if (finishPointLocation[i] != 0
                    && nodeModelList[i].ChipModel == null)
                {
                    _stateMachine.Enter<SelectFirstNodeState>();
                    break;
                }

                if (finishPointLocation[i] == 0
                    && nodeModelList[i].ChipModel == null)
                {
                    matchesArray[i] = true;
                    continue;
                }

                if (finishPointLocation[i] == nodeModelList[i].ChipModel.ID)
                {
                    matchesArray[i] = true;
                }
            }
        }

        private bool IsFinishState(bool[] matchesArray)
        {
            var flag = true;
            foreach (var match in matchesArray)
            {
                if (!match)
                {
                    flag = false;
                    break;
                }
            }

            return flag;
        }

        public void OnExit()
        {
        }
    }
}