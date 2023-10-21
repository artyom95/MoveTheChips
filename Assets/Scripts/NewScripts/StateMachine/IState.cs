

using NewScripts.StateMachine;


public interface IState <T> where T : class
{
    void Initialize(StateMachine <T> stateMachine, T gameContext);
    void OnEnter();
    void OnExit();
}
