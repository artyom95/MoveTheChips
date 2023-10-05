using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class DataDFSLoader : MonoBehaviour
{
    public event Action<List<Vector2>> OnMovingPlaceFound;
    [SerializeField] private PositionCalculator _positionCalculator;

    private int[,] _chipsArray;
    private GameSettings _gameSettings;
    private List<Vector2> _highlightPlacesPositions = new();
    private int[,] _graphsArray;

    private bool[] _uses;

    private List<Vector2> _coordinatesPoints = new();
    private List<int> _initialPointLocation = new();
    private List<Vector2> _connectionsBetweenPointsPairs = new();
    private int _amountPoints;

    public void Initialize(List<Vector2> coordinatesPoints,
        List<int> initialPointLocation, List<Vector2> connectionsBetweenPointsPairs, int amountPoints)
    {
        _coordinatesPoints = coordinatesPoints;
        _initialPointLocation = initialPointLocation;
        _connectionsBetweenPointsPairs = connectionsBetweenPointsPairs;
        _amountPoints = amountPoints;
        CreateGraphArray();
        CreateUsesArray();
        CreateArrayWithChipsValue();
    }

    public int[,] GetChipsArray()
    {
        return _chipsArray;
    }

    private void CreateGraphArray()
    {
        var sizeGraphArray = _amountPoints + 1;
        _graphsArray = new int[sizeGraphArray, sizeGraphArray];
        FillGraphArray();
    }

    private void CreateUsesArray()
    {
        var sizeUsesArray = _amountPoints + 1;
        _uses = new bool[sizeUsesArray];
    }

    private void FillGraphArray()
    {
        foreach (var vector2 in _connectionsBetweenPointsPairs)
        {
            _graphsArray[(int)vector2.x, (int)vector2.y] = 1;
            _graphsArray[(int)vector2.y, (int)vector2.x] = 1;
        }
    }

    private void CreateArrayWithChipsValue()
    {
        var chipsArrayCount = _amountPoints / 3;
        _chipsArray = new int[chipsArrayCount, chipsArrayCount];
        FillArrayWithChipsValue();
    }

    private void FillArrayWithChipsValue()
    {
        var value = 0;
        for (var i = 0; i < _chipsArray.GetLength(0); i++)
        {
            for (var j = 0; j < _chipsArray.GetLength(1); j++)
            {
                value++;
                if (AreValuesEqual(value))
                {
                    _chipsArray[i, j] = value;
                }
            }
        }
    }

    private bool AreValuesEqual(int value)
    {
        foreach (var location in _initialPointLocation.Where(location => value == location))
        {
            return true;
        }

        return false;
    }

    [UsedImplicitly]
    public void FindMovingPlace(Chip chip)
    {
        var startPlace = _positionCalculator.ConvertWorldToCell(chip.CurrentCoordinate);
        var startPoint = 0;
        var position = new Vector2(startPlace.x, startPlace.z);
        for (var i = 0; i < _coordinatesPoints.Count; i++)
        {
            if (_coordinatesPoints[i].Equals(position))
            {
                startPoint = i + 1;
                break;
            }
        }

        DFS(startPoint);
        if (_highlightPlacesPositions.Count > 0)
        {
            OnMovingPlaceFound?.Invoke(_highlightPlacesPositions);
        }

        Array.Clear(_uses, 0, _uses.Length);
    }

    private void DFS(int start)
    {
        if (_uses[start])
        {
            return;
        }

        _uses[start] = true;
        for (int i = 0; i < _graphsArray.GetLength(0); i++)
        {
            if (_graphsArray[start, i] > 0 && FindValueCellInChipsArray(i) == 0
                                           && !_uses[i])
            {
                _highlightPlacesPositions.Add(_coordinatesPoints[i - 1]);
                DFS(i);
            }
        }
    }

    private int FindValueCellInChipsArray(int numberCell)
    {
        var value = 0;
        for (var i = 0; i < _chipsArray.GetLength(0); i++)
        {
            for (var j = 0; j < _chipsArray.GetLength(1); j++)
            {
                value++;
                if (value == numberCell)
                {
                    return _chipsArray[i, j];
                }
            }
        }

        return default;
    }
}