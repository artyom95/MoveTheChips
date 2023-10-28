using System.Collections.Generic;
using UnityEngine;

public class PositionCalculator : MonoBehaviour
{
    [SerializeField] private Grid _grid;

    public Vector3 CalculatePositionForInstance(Vector2 vector2, int position = 0)
    {
        var convertPosition = new Vector3Int((int)vector2.x, position, (int)vector2.y);
        var positionForInstance = _grid.CellToWorld(convertPosition);
        return positionForInstance;
    }


    public Vector3Int ConvertWorldToCell(Vector3 worldPosition)
    {
        var cellPosition = _grid.WorldToCell(worldPosition);
        var newPosition = new Vector3Int(cellPosition.x, cellPosition.y, -cellPosition.z);
        return newPosition;
    }

    public Vector3 ConvertCellToWorld(Vector2 vector2)
    {
        Vector3Int cellPosition = new Vector3Int((int)vector2.x, 0, -(int)vector2.y);
        var worldPosition = _grid.CellToWorld(cellPosition);
        return worldPosition;
    }

    public Vector3 CalculatePositionForDFSFind(Vector3 chipPosition)
    {
        var positionForDFSFind = _grid.WorldToCell(new Vector3(chipPosition.x, 0, chipPosition.z));
        return positionForDFSFind;
    }

    public List<Vector3> CalculatePositionChipForHighlighting(List<Vector2> chipsPositions)
    {
        var listPositionForHighilighting = new List<Vector3>();
        foreach (var position in chipsPositions)
        {
            var positionChipForHighlighting = _grid.CellToWorld(new Vector3Int((int)position.x, 0, -(int)position.y));
            listPositionForHighilighting.Add(positionChipForHighlighting);
        }

        return listPositionForHighilighting;
    }
}