using JetBrains.Annotations;
using NewScripts.StateMachine;
using UnityEngine;

public class FinishGameState :  IState<GameContext>
{
    private readonly GameOverController _gameOverController;
    
    private StateMachine<GameContext> _stateMachine;
    private GameContext _gameContext;

    public FinishGameState(GameOverController gameOverController)
    {
        _gameOverController = gameOverController;
    }
    public void Initialize(StateMachine<GameContext> stateMachine, GameContext gameContext)
    {
        _gameContext = gameContext;
        _stateMachine = stateMachine;
    }

    public void OnEnter()
    {
        var count = 0;
        var isFinish = false;
        for (var i = 0; i < _gameContext.NodeModelsList.Count; i++)
        {
            if (_gameContext.NodeModelsList[i].ChipModel == null)
            {
                continue;
            }

            if (_gameContext.NodeModelsList[i].ChipModel.ID == _gameContext.FinishPointLocation[count])
            {
                if (i == _gameContext.NodeModelsList.Count - 1)
                {
                    isFinish = true;
                }

                count++;
            }
            else
            {
                _stateMachine.Enter<SelectFirstNodeState>();
                break;
            }
        }

        if (isFinish)
        {
            _gameOverController.ShowWinScreen();
            Debug.Log("You Win!!! Congrats");
        }
    }

    [UsedImplicitly]
    public void LoadStartState()
    {
        _stateMachine.Enter<StartLoadState>();
    }

    [UsedImplicitly]
    public void LoadSelectState()
    {
        _stateMachine.Enter<SelectFirstNodeState>();
    }

    public void OnExit()
    {
    }
}