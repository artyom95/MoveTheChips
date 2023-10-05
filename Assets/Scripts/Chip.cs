using System.Collections.Generic;
using UnityEngine;


public class Chip : MonoBehaviour, IHasColor
{
    public Vector3 CurrentCoordinate { get; private set; }
    
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private OutlineController _outlineController;
    [SerializeField] private List<Material> _materials;

    public void SetCoordinate(Vector3 coordinate)
    {
        CurrentCoordinate = coordinate;
    }

    public void SetColor(Color color)
    {
        _meshRenderer.material.color = color;
    }

    public void SetOutline()
    {
        _outlineController.SetFocus();
    }

    public void ResetOutline()
    {
        _outlineController.RemoveFocus();
    }
}