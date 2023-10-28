
using NewScripts.Chip;

namespace NewScripts.StateMachine
{
    public class ChipMovementState :  IState<GameContext>
    {
        private  ChipMover _chipMover { get; }

       
        private StateMachine<GameContext> _stateMachine;
        private GameContext _gameContext;

        public ChipMovementState(ChipMover chipMover)
        {
            _chipMover = chipMover;
        }
        public void Initialize(StateMachine<GameContext> stateMachine, GameContext gameContext)
        {
            _gameContext = gameContext;
            _stateMachine = stateMachine;
        }

        public void OnEnter()
        {
            var chip = _gameContext.StartNodeModel.ChipModel;
            var path = _gameContext.Path.ToArray();
            foreach (var nodeModel in _gameContext.HighlightingNodesList)
            {
                nodeModel.TurnOffOutline();
            }

            _gameContext.StartNodeModel.ChipModel.TurnOffOutline();
          /*  var sequence = DOTween.Sequence();

            sequence.Join(chip.transform.DOPath(path, _duration))
                .AppendCallback(() => _gameContext.StartNodeModel.ResetChipModel())
                .AppendCallback(() => _gameContext.FinishNodeModel.SetChipModel(chip))
                .AppendCallback(() => _stateMachine.Enter<FinishGameState>());
        */
          
          _chipMover.StartMove(path, chip, _gameContext,_stateMachine);
          _gameContext.Chip = chip;
        }
        
        public void OnExit()
        {
        }
    }
}