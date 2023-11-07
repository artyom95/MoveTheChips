using System.Collections.Generic;
using NewScripts.Node;
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
        var matchesArray = new bool[_gameContext.FinishPointLocation.Count];
        var nodeModelList = _gameContext.NodeModelsList;
        var finishPointLocation = _gameContext.FinishPointLocation;
       

        CheckOutMatches(finishPointLocation,nodeModelList, matchesArray);

        var isFinish =  IsItFinishState(matchesArray);

        if (isFinish)
        {
            _gameOverController.ShowWinScreen();
            Debug.Log("You Win!!! Congrats");
        }
        else
        {
            _stateMachine.Enter<SelectFirstNodeState>();
        }
    }
    private void CheckOutMatches(List<int> finishPointLocation,List<NodeModel> nodeModelList, bool[] matchesArray)
    {
        
        for (var i = 0; i < finishPointLocation.Count; i++)
        {
            if (finishPointLocation[i] != 0
                && nodeModelList[i].ChipModel == null)
            {
                _stateMachine.Enter<SelectFirstNodeState>();
                break;
            }

            if (finishPointLocation[i] == 0
                && nodeModelList[i].ChipModel == null)
            {
                matchesArray[i] = true;
                continue;
            }

            if (finishPointLocation[i] == nodeModelList[i].ChipModel.ID)
            {
                matchesArray[i] = true;

            }
        }

    }

    private bool IsItFinishState( bool[] matchesArray)
    {
        var flag = false;
        foreach (var match in matchesArray)
        {
            if (!match)
            {
                flag= false;
                break;
            }
            else
            {
                flag = true;
            }
        }

        return flag;
    }
    public void OnExit()
    {
    }
}