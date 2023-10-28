using NewScripts.StateMachine;
using UnityEngine;

public class FinishGameState : IState<GameContext>
{
    private readonly GameOverController _gameOverController;

    private StateMachine<GameContext> _stateMachine;
    private GameContext _gameContext;

    private bool _isFillFields;

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
        bool isFinish = false;
        var matchesArray = new bool[_gameContext.FinishIndexChips.Length];


        for (int i = 0; i < _gameContext.NodeModelsList.Count; i++)
        {
            if (_gameContext.NodeModelsList[i].ChipModel == null
                && _gameContext.FinishIndexChips[i] == 0)
            {
                matchesArray[i] = true;
                continue;
            }

            if (_gameContext.NodeModelsList[i].ChipModel != null &&
                _gameContext.NodeModelsList[i].ChipModel.ID == _gameContext.FinishIndexChips[i])
            {
                matchesArray[i] = true;
            }
            else
            {
                _stateMachine.Enter<SelectFirstNodeState>();
                break;
            }
        }

        foreach (var variable in matchesArray)
        {
            if (!variable)
            {
                isFinish = false;
                break;
            }
            else
            {
                isFinish = true;
            }
        }

        if (isFinish)
        {
            _gameOverController.ShowWinScreen();
            Debug.Log("You Win!!! Congrats");
        }
    }

    public void OnExit()
    {
    }
}