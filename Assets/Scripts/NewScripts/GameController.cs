using System;
using System.Collections.Generic;
using NewScripts.StateMachine;
using UnityEngine;

namespace NewScripts
{
    [Serializable]
    public class GameController : MonoBehaviour

    {
        [SerializeField] private SelectFirstNodeState _selectFirstNodeState;
        [SerializeField] private SelectSecondNodeState _selectSecondNodeState;
        [SerializeField] private ChipMovementState _chipMovementState;
        [SerializeField] private FinishGameState _finishGameState;
        [SerializeField] private StartLoadState _startLoadState;
        
        private void Start()
        {

            var stateMachine = new StateMachine<GameContext>
            (
                _startLoadState,
                _selectFirstNodeState,
                _selectSecondNodeState,
                _chipMovementState,
                _finishGameState
            );
            stateMachine.Initialize(new GameContext());
            stateMachine.Enter<StartLoadState>();
        }
        
    }
}