using System;
using NewScripts.StateMachine;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace NewScripts
{
   
    public class GameController : IStartable
    {
        private readonly StartLoadState _startLoadState;
        private readonly SelectFirstNodeState _selectFirstNodeState;
        private readonly SelectSecondNodeState _selectSecondNodeState;
        private readonly FinishGameState _finishGameState;
        private readonly ChipMovementState _chipMovementState;
       // private StateMachine<GameContext> _stateMachine;
        public GameController(//StateMachine<GameContext> stateMachine,
            StartLoadState startLoadState,
            SelectFirstNodeState selectFirstNodeState,
            SelectSecondNodeState selectSecondNodeState,
            FinishGameState finishGameState,
            ChipMovementState chipMovementState, IObjectResolver builder)
        {
            //_stateMachine = stateMachine;
            _startLoadState = startLoadState;
            _selectFirstNodeState = selectFirstNodeState;
            _selectSecondNodeState = selectSecondNodeState;
            _finishGameState = finishGameState;
            _chipMovementState = chipMovementState;

            
        }

        void IStartable.Start()
        {

           var stateMachine = new StateMachine<GameContext>
            (
                _startLoadState,
                _selectFirstNodeState,
                _selectSecondNodeState,
                _finishGameState,
                _chipMovementState
            );
            stateMachine.Initialize(new GameContext());
           
           stateMachine.Enter<StartLoadState>();
        }
        
    }
}