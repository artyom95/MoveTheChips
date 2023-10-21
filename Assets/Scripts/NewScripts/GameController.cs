using System;
using System.Collections.Generic;
using NewScripts.StateMachine;
using NewScripts.UIScripts;
using UnityEngine;
using UnityEngine.Serialization;

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


        // [SerializeField] private ChipSelector _chipSelector;
        // [SerializeField] private ChipMover _chipMover;
        // [SerializeField] private ScreenDisplayLoader _screenDisplayLoader;
        private List<Chip> _listChips = new List<Chip>();

        private List<Chip> _listPlaces = new List<Chip>();

      
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

            //  _listPlaces = _screenDisplayLoader.Initialize(_coordinatesPoints,_colorList,_initialPointLocation,_connectionsBetweenPointsPairs);
            //  _listChips = _screenDisplayLoader.InstallChips();
            //  SubscribeMethods();

            //   _chipMover.Initialize(_chipsArray, _coordinatesPoints);
        }

        //  private void SubscribeMethods()
        //  {
        //      _chipSelector.ChipSelected += _chipMover.GetMovementObject;
        //      _chipSelector.PlaceForChipSelected += _chipMover.GetDestinationPlace;
        //      _chipSelector.PlaceSelected += _screenDisplayLoader.TurnOffChipsForClick;
        //      _chipMover.MoveCompleted += _chipSelector.ChangeStateSelector;

        //  }
        // Update is called once per frame
        //    private void OnDestroy()
        //    {
//
        //        _chipSelector.ChipSelected -= _chipMover.GetMovementObject;
        //        _chipSelector.PlaceForChipSelected -= _chipMover.GetDestinationPlace;
        //    
        //        _chipSelector.PlaceSelected -= _screenDisplayLoader.TurnOffChipsForClick;
//
        //    
        //        _chipMover.MoveCompleted -= _chipSelector.ChangeStateSelector;
        //  
        //    }
//
    }
}