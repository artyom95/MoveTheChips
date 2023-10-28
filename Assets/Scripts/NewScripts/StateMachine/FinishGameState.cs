using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using NewScripts;
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
       

        var count = 0;
        bool isFinish = false;
        var matchesArray = new bool[_gameContext.FinishIndexChips.Length];
       /* for (var i = 0; i < _gameContext.NodeModelsList.Count; i++)
        {
            if (_gameContext.NodeModelsList[i].ChipModel == null)
            {
                continue;
            }


            // _gameContext.NodeModelsList[i].ChipModel.ID == _gameContext.FinishPointLocation[count]
            if (_gameContext.NodeModelsList[i].Position == _gameContext.Chip.Position)
            {
                if (i == _gameContext.NodeModelsList.Count - 1)
                {
                    isFinish = true;
                    break;
                }

                count++;
            }
            else
            {
                _stateMachine.Enter<SelectFirstNodeState>();
                break;
            }
        }*/

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
               continue;
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

    public void OnExit()
    {
    }
}