using System;
using System.Collections.Generic;


namespace NewScripts.StateMachine
{

    public class StateMachine<TGameContext>  where TGameContext: class 
    {
        private readonly Dictionary<Type, IState<TGameContext>> _states = new();
        private IState<TGameContext> _currentState;

        public StateMachine(params IState<TGameContext>[] states)
        {
            foreach (var state in  states)
            {
                _states[state.GetType()] = state;
            }
        }

        public void Initialize( TGameContext gameContext)
        {
            foreach (var statePairs in _states)
            {
                statePairs.Value.Initialize(this, gameContext);
            }
        }

        public void Enter<TState>() where TState : IState <TGameContext>
        {
            _currentState?.OnExit();

            _currentState = _states[typeof(TState)];
        
            _currentState.OnEnter();
        }
    }

   
}
