using System;
using Cysharp.Threading.Tasks;
using NewScripts.Chip;
using NewScripts.Events;
using NewScripts.Node;
using UniTaskPubSub;

namespace NewScripts.StateMachine
{
    public class ChipMovementState : IState<GameContext>
    {
        private ChipMover _chipMover { get; }


        private StateMachine<GameContext> _stateMachine;
        private GameContext _gameContext;
        private readonly AsyncMessageBus _messageBus;
        private IDisposable _subscription;
        private readonly NodeSelector _nodeSelector;

        public ChipMovementState(ChipMover chipMover, AsyncMessageBus messageBus, NodeSelector nodeSelector)
        {
            _nodeSelector = nodeSelector;
            _messageBus = messageBus;
            _chipMover = chipMover;
        }

        public void Initialize(StateMachine<GameContext> stateMachine, GameContext gameContext)
        {
            _gameContext = gameContext;
            _stateMachine = stateMachine;
        }

       
        public async UniTask OnEnter()
        {
            _subscription = _messageBus.Subscribe<FinishChipMovingEvent>(_ => _stateMachine.Enter<FinishGameState>());
            _nodeSelector.ChangeStateNodeSelector(true);
            var chip = _gameContext.StartNodeModel.ChipModel;
            var path = _gameContext.Path.ToArray();
            _gameContext.CurrentChip = chip;
            SwitchOutline();
            await _chipMover.StartMove(path, chip, _gameContext, _stateMachine);
        }

        private void SwitchOutline()
        {
            foreach (var nodeModel in _gameContext.HighlightingNodesList)
            {
                nodeModel.TurnOffOutline();
            }

            _gameContext.StartNodeModel.ChipModel.TurnOffOutline();
        }

        public void OnExit()
        {
           
            _subscription.Dispose();
        }
    }
}