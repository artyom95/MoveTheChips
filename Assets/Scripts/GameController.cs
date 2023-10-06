using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private DataDFSLoader _dataDfsLoader;

    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private ChipSelector _chipSelector;
    [SerializeField] private ChipMover _chipMover;
    [SerializeField] private ScreenDisplayLoader _screenDisplayLoader;
    private List<Chip> _listChips = new List<Chip>();

    private List<Chip> _listPlaces = new List<Chip>();

    private List<Vector2> _coordinatesPoints = new ();
    private List<Color> _colorList = new ();
    private List<int> _initialPointLocation = new();
    private List<Vector2> _connectionsBetweenPointsPairs = new();
    private int _amountPoints;
    private int[,] _chipsArray;
    private void Start()
    {
        FillFields();
        _dataDfsLoader.Initialize(_coordinatesPoints,_initialPointLocation,_connectionsBetweenPointsPairs, _amountPoints);
        _listPlaces = _screenDisplayLoader.Initialize(_coordinatesPoints,_colorList,_initialPointLocation,_connectionsBetweenPointsPairs);
        _listChips = _screenDisplayLoader.InstallChips();
        SubscribeMethods();
        
        _chipsArray = _dataDfsLoader.GetChipsArray();
        _chipMover.Initialize(_chipsArray, _coordinatesPoints);

    }

    private void SubscribeMethods()
    {
        _chipSelector.ChipSelected += _dataDfsLoader.FindMovingPlace;
        _chipSelector.ChipSelected += _chipMover.GetMovementObject;
        _chipSelector.PlaceForChipSelected += _chipMover.GetDestinationPlace;
        _chipSelector.PlaceSelected += _screenDisplayLoader.TurnOffChipsForClick;
        _dataDfsLoader.MovingPlaceFound += _screenDisplayLoader.ShowFoundChipForClick;
        _dataDfsLoader.MovingPlaceNotFound += _chipSelector.ResetChip;
        _chipMover.MoveCompleted += _chipSelector.ChangeStateSelector;

    }
    // Update is called once per frame
    private void OnDestroy()
    {
        _chipSelector.ChipSelected -= _dataDfsLoader.FindMovingPlace;
        _dataDfsLoader.MovingPlaceFound -= _screenDisplayLoader.ShowFoundChipForClick;
        _dataDfsLoader.MovingPlaceNotFound -= _chipSelector.ResetChip;

        _chipSelector.ChipSelected -= _chipMover.GetMovementObject;
        _chipSelector.PlaceForChipSelected -= _chipMover.GetDestinationPlace;
        
        _chipSelector.PlaceSelected -= _screenDisplayLoader.TurnOffChipsForClick;

        
        _chipMover.MoveCompleted -= _chipSelector.ChangeStateSelector;
      
    }

    private void FillFields()
    {
        _coordinatesPoints = _gameSettings.ScriptableSettings[0].CoordinatesPoints;
        _colorList = _gameSettings.ScriptableSettings[0].ColorsChips;
        _initialPointLocation = _gameSettings.ScriptableSettings[0].InitialPointLocation;
        _connectionsBetweenPointsPairs = _gameSettings.ScriptableSettings[0].ConnectionsBetweenPointPairs;
        _amountPoints = _gameSettings.ScriptableSettings[0].AmountPoints;
    }
}