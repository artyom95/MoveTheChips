using DG.Tweening;
using UnityEngine;

namespace NewScripts.StateMachine
{
    public class ChipMovementState : MonoBehaviour, IState<GameContext>
    {
        [SerializeField] private float _duration;

        private StateMachine<GameContext> _stateMachine;
        private GameContext _gameContext;


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
            var sequence = DOTween.Sequence();

            sequence.Join(chip.transform.DOPath(path, _duration))
                .AppendCallback(() => _gameContext.StartNodeModel.ResetChipModel())
                .AppendCallback(() => _gameContext.FinishNodeModel.SetChipModel(chip))
                .AppendCallback(() => _stateMachine.Enter<FinishGameState>());
        }

        public void OnExit()
        {
        }
    }
}