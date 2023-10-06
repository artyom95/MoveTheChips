using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class ChipMover : MonoBehaviour
{
    public event Action MoveCompleted;

    [SerializeField] private float _movingDuration;
    [SerializeField] private PositionCalculator _positionCalculator;
    private Chip _chip;
    private Chip _chipPlace;

    private int[,] _chipsArray;
    private List<Vector2> _coordinatesPoint;
    private List<Vector2> _coordinatesPointsPath;
    private List<Vector2> _correctedCoordinatesPointsPath = new();
    private int _chipNumber;


    public void Initialize(int[,] chipsArray, List<Vector2> coordinatesPoint)
    {
        _chipsArray = chipsArray;
        _coordinatesPoint = coordinatesPoint;
    }

    public void GetMovementObject(Chip chip)
    {
        _chip = chip;
    }

    public void GetDestinationPlace(Chip chipPlace)
    {
        _chipPlace = chipPlace;
        FindPath();
    }

    private void MoveChip(List<Vector3> path)
    {
        var arrayPath = path.ToArray();
        var pathMode = PathMode.TopDown2D;
        var pathType = PathType.Linear;
        var indexFirstPlaceChipWorld = _positionCalculator.ConvertWorldToCell(_chip.CurrentCoordinate);
        var indexFirstPlaceChipCell = new Vector2(indexFirstPlaceChipWorld.x, indexFirstPlaceChipWorld.z);
        var sequence = DOTween.Sequence();
        sequence.Append(_chip.transform.DOPath(arrayPath, _movingDuration, pathType, pathMode));
           sequence.AppendCallback(() => MoveCompleted?.Invoke());
               sequence.AppendCallback(() => _chip.SetCoordinate(arrayPath[^1]));
                   sequence.AppendCallback(() => ChangeChipsArray(indexFirstPlaceChipCell));
    }

    private void ChangeChipsArray(Vector2 indexFirstPlaceChipCell)
    {
        var valueChip = FindValueChip(indexFirstPlaceChipCell);
       for (var i = 0; i < _chipsArray.GetLength(0); i++)
       {
           for (var i1 = 0; i1 < _chipsArray.GetLength(1); i1++)
           {
               if (_chipsArray [i,i1] == valueChip)
               {
                   _chipsArray[i, i1] = 0;
                   break;
               }
           }
       }
        var indexLastPlace = _coordinatesPointsPath.Last();
        _chipsArray[(int)indexLastPlace.x, (int)indexLastPlace.y] = valueChip;
    }

    private int FindValueChip(Vector2 indexFirstPlaceChipCell )
    {
        var numberPoint = 0;
        foreach (var vector2 in _coordinatesPoint)
        {
            numberPoint++;
            if (vector2.Equals(indexFirstPlaceChipCell))
            {
                return numberPoint;
            }
        }

        return default;
    }

    private Vector2 FindFinishPositionInArray()
    {
        var vector3PositionInCell = _positionCalculator.ConvertWorldToCell(_chipPlace.CurrentCoordinate);
        var vector2PositionInCell = new Vector2(vector3PositionInCell.x, vector3PositionInCell.z);
        var numberPairsPositionInCellAtCoordinatesPoint = 0;
        foreach (var vector2 in _coordinatesPoint)
        {
            numberPairsPositionInCellAtCoordinatesPoint++;
            if (vector2.Equals(vector2PositionInCell))
            {
                break;
            }

        }

        var numberPairsPositionInCellAtChipsArray = 0;
        for (var i = 0; i < _chipsArray.GetLength(0); i++)
        {
            for (var i1 = 0; i1 < _chipsArray.GetLength(1); i1++)
            {
                numberPairsPositionInCellAtChipsArray++;
                if (numberPairsPositionInCellAtCoordinatesPoint == numberPairsPositionInCellAtChipsArray)
                {
                    return new Vector2(i, i1);
                }
            }
        }

        return default;
    }
    private void FindPath()
    {
        var finishPosition = FindFinishPositionInArray();
        var coordinatesPointsPath = new List<Vector2>();
        int n = _chipsArray.GetLength(0);
        int m = _chipsArray.GetLength(1);

        Queue<Vector2> points = new Queue<Vector2>();

        int[][] labirint = new int[n + 2][];
        for (int index = 0; index < n + 2; index++)
        {
            labirint[index] = new int[m + 2];
        }


        for (int i = 0; i <= n + 1; i++)
        {
            for (int j = 0; j <= m + 1; j++)
            {
                labirint[i][j] = -1;
            }
        }

        var position = new Vector2();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < m; j++)
            {
                var index = _chipsArray[i, j];
                if (index > 0)
                {
                    position = _coordinatesPoint[index - 1];
                }
                else
                {
                    labirint[i + 1][j + 1] = 0;
                    continue;
                }

                var chipPosition = _positionCalculator.ConvertWorldToCell(_chip.CurrentCoordinate);
                if (position.Equals(new Vector2(chipPosition.x, chipPosition.z)))
                {
                    labirint[i + 1][j + 1] = -10;
                    points.Enqueue(new Vector2(i + 1, j + 1));
                }
                else if (_chipsArray[i, j] > 1)
                {
                    labirint[i + 1][j + 1] = -1;
                }
            }
        }

        var step = 1;
        var start = 1;
        var end = 1;
        var count = 1;

        _coordinatesPointsPath = coordinatesPointsPath;
        while (true)
        {
            for (int i = start; i <= end; i++)
            {
                var currentCoordinates = points.Dequeue();
                var x = (int)currentCoordinates.x;
                var y = (int)currentCoordinates.y;
                if (labirint[x - 1][y] == 0)
                {
                    labirint[x - 1][y] = step;
                    points.Enqueue(new Vector2(x - 1, y));
                    count++;
                    _coordinatesPointsPath.Add(new Vector2(x - 1, y - 1));
                }

                if (labirint[x + 1][y] == 0)
                {
                    labirint[x + 1][y] = step;
                    points.Enqueue(new Vector2(x + 1, y));
                    count++;
                    _coordinatesPointsPath.Add(new Vector2(x, y - 1));
                }

                if (labirint[x][y - 1] == 0)
                {
                    labirint[x][y - 1] = step;
                    points.Enqueue(new Vector2(x, y - 1));
                    count++;
                    _coordinatesPointsPath.Add(new Vector2(x - 1, y - 2));
                }

                if (labirint[x][y + 1] == 0)
                {
                    labirint[x][y + 1] = step;
                    points.Enqueue(new Vector2(x, y + 1));
                    count++;
                    _coordinatesPointsPath.Add(new Vector2(x - 1, y));
                }
            }

            if (points.Count == 0)
            {
                if (labirint[(int)finishPosition.x+1][(int)finishPosition.y+1] > 0)
                {
                    var labirintValueAtFinish = labirint[(int)finishPosition.x+1][(int)finishPosition.y+1];
                  CorrectCoordinatePointsPath(labirint, finishPosition, labirintValueAtFinish);  
                }
                break;
            }

            start = end + 1;
            end = count;
            step++;
        }

        FillListChipsPosition();
    }

    private void CorrectCoordinatePointsPath(int[][] labirint, Vector2 finishPosition, int finishValue)
    {
        for (var i = 0; i < labirint.Length; i++)
        {
            for (var i1 = 0; i1 < labirint.Length; i1++)
            {
                if (labirint[i][i1] == finishValue && (finishPosition.x+1 != i || finishPosition.y+1!=i1))
                {
                   CorrectCoordinatePointsPath(new Vector2(i-1,i1-1));
                }
            }
        }
    }

    private void  CorrectCoordinatePointsPath(Vector2 positionInArray)
    {
        foreach (var vector2 in _coordinatesPointsPath)
        {
            if (vector2 != positionInArray)
            {
                _correctedCoordinatesPointsPath.Add(vector2);
            } 
        }

        _coordinatesPointsPath = _correctedCoordinatesPointsPath;
    }
    private void FillListChipsPosition()
    {
        var pathForChipMoving = new List<Vector2>();
        foreach (var location in _coordinatesPointsPath)
        {
            if (AreValueEqual(location))
            {
                pathForChipMoving.Add(_coordinatesPoint[_chipNumber - 1]);
            }
        }

        ConvertPathForChipMoving(pathForChipMoving);
    }

    private bool AreValueEqual(Vector2 vector2)
    {
        _chipNumber = 0;

        for (var i = 0; i < _chipsArray.GetLength(0); i++)
        {
            for (var j = 0; j < _chipsArray.GetLength(1); j++)
            {
                _chipNumber++;
                var indexArray = new Vector2(i, j);
                if (indexArray.Equals(vector2))
                {
                    return true;
                }
            }
        }

        return false;
    }

    private void ConvertPathForChipMoving(List<Vector2> pathForChipMoving)
    {
        var convertPathForChipMoving = new List<Vector3>();
        foreach (var vector2 in pathForChipMoving)
        {
            convertPathForChipMoving.Add(_positionCalculator.ConvertCellToWorld(vector2));
        }

        MoveChip(convertPathForChipMoving);
    }
}