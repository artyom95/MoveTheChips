using JetBrains.Annotations;
using NewScripts.StateMachine;
using UnityEngine;
using VContainer;

public class ButtonPresenter : MonoBehaviour
{
    private StateMachine<GameContext> _stateMachine;

    [Inject]
    public void Construct(StateMachine<GameContext> stateMachine)
    {
        _stateMachine = stateMachine;
    }

    [UsedImplicitly]
    public void LoadNextState()
    {
        _stateMachine.Enter<StartLoadState>();
    }

    [UsedImplicitly]
    public void LoadSelectState()
    {
        _stateMachine.Enter<SelectFirstNodeState>();
    }
}