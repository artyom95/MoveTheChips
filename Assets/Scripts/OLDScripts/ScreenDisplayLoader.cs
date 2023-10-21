using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class ScreenDisplayLoader : MonoBehaviour
{
    [SerializeField] private Chip _colorChipPrefab;
    [SerializeField] private Chip _chipPrefab;

    [SerializeField] private PositionCalculator _positionCalculator;

    [SerializeField] private GameObject _horizontalPrefabWall;
    [SerializeField] private GameObject _verticalPrefabWall;

    private readonly List<Chip> _listChips = new List<Chip>();
    private readonly List<Chip> _listPlaces = new List<Chip>();
    
   
    private List<Vector2> _coordinatesPoints = new ();
    private List<Color> _colorList = new ();
    private List<int> _initialPointLocation = new();
    private List<Vector2> _connectionsBetweenPointsPairs = new();
    

   
    private List<Chip> _listChipsForClick;

    public List<Chip> Initialize( List<Vector2> coordinatesPoints, List<Color> colorList,
        List<int> initialPointLocation, List<Vector2> connectionsBetweenPointsPairs)
    {
        _coordinatesPoints = coordinatesPoints;
        _colorList = colorList;
        _initialPointLocation = initialPointLocation;
        _connectionsBetweenPointsPairs = connectionsBetweenPointsPairs;
        InstallWallsBetweenChips();
        InstallAllPlacesForChips();
        return _listPlaces;
    }

    private void InstallAllPlacesForChips()
    {
        foreach (var coordinatesPoint in _coordinatesPoints)
        {
           var positionPlaceForChip = _positionCalculator.ConvertCellToWorld(coordinatesPoint);
  
          var place = Instantiate(_chipPrefab);
            place.transform.position = positionPlaceForChip;
            place.gameObject.SetActive(false);
            place.SetCoordinate(positionPlaceForChip);
            _listPlaces.Add(place);
        }
    }

    public List<Chip> InstallChips()
    {
        var index = 0;
        foreach (var point in _initialPointLocation)
        {
            var indexChipPosition = point - 1;
            var chip = Instantiate(_colorChipPrefab);
            var position = _positionCalculator.ConvertCellToWorld(_coordinatesPoints[indexChipPosition]);
                   
          chip.transform.position = position;
            chip.SetCoordinate(position);
            chip.SetColor(_colorList[index]);
            _listChips.Add(chip);
            index++;
        }

        return _listChips;
    }

    public void InstallWallsBetweenChips()
    {
        
        var firstIndexChip = 0;
        var secondIndexChip = 0;
        var distance = new Vector2();
        var position = new Vector3();
        foreach (var vector in _connectionsBetweenPointsPairs)
        {
            firstIndexChip = (int)vector.x;
            secondIndexChip = (int)vector.y;
// расчет положения перенести в position calculator
            if (_coordinatesPoints[secondIndexChip - 1].x == _coordinatesPoints[firstIndexChip - 1].x)
            {
                var x = _coordinatesPoints[secondIndexChip - 1].x;
                var y = (_coordinatesPoints[secondIndexChip - 1].y + _coordinatesPoints[firstIndexChip - 1].y) / 2;
                distance = new Vector2(x, y);
            }
            else
            {
                var x = (_coordinatesPoints[secondIndexChip - 1].x + _coordinatesPoints[firstIndexChip - 1].x) / 2;
                var y = _coordinatesPoints[secondIndexChip - 1].y;
                distance = new Vector2(x, y);
            }

            position = _positionCalculator.ConvertCellToWorld(distance);
                   if (Math.Abs(firstIndexChip - secondIndexChip) == 3)
            {
                var wall = Instantiate(_verticalPrefabWall);
                position.z -= 0.5f;
                position.y -= 0.01f;
                wall.transform.position = position;
            }

            if (Math.Abs(firstIndexChip - secondIndexChip) == 1)
            {
                var wall = Instantiate(_horizontalPrefabWall);
                position.x += 0.5f;
                position.y -= 0.01f;
                wall.transform.position = position;
            }
        }
    }

    [UsedImplicitly]
    public void ShowFoundChipForClick(List<Vector2> chipsPositions)
    {
        var positionChipList = _positionCalculator.CalculatePositionChipForHighlighting(chipsPositions);
        _listChipsForClick = new List<Chip>();
        for (var i = 0; i < _listPlaces.Count; i++)
        {
            for (var i1 = 0; i1 < positionChipList.Count; i1++)
            {
                if (_listPlaces[i].CurrentCoordinate.Equals(positionChipList[i1]))
                {
                    _listChipsForClick.Add(_listPlaces[i]);
                }
            }
        }
        
        foreach (var chip in _listChipsForClick)
        {
          chip.gameObject.SetActive(true);
          chip.SetOutline();
        }
    }

    public void TurnOffChipsForClick()
    {
        foreach (var chip in _listChipsForClick)
        {
            chip.ResetOutline();
            chip.gameObject.SetActive(false);
        }
    }
}