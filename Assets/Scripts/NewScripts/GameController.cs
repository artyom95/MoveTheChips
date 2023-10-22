using System;
using NewScripts.StateMachine;
using VContainer.Unity;

namespace NewScripts
{
    [Serializable]
    public class GameController : IStartable
    {
        private StateMachine<GameContext> _stateMachine;

        public GameController(StateMachine< GameContext> stateMachine)
        {
            _stateMachine = stateMachine;
        }
        void IStartable.Start()
        {

            _stateMachine.Enter<StartLoadState>();
        }
        
    }
}