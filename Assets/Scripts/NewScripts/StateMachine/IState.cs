using Cysharp.Threading.Tasks;

namespace NewScripts.StateMachine
{
    public interface IState<T> where T : class
    {
        void Initialize(StateMachine<T> stateMachine, T gameContext);
        UniTask OnEnter();
        void OnExit();
    }
}