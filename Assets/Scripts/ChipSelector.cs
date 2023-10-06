using System;
using UnityEngine;


public class ChipSelector : MonoBehaviour
{
    public event Action<Chip > ChipSelected;

    public event Action<Chip> PlaceForChipSelected;
    public event Action PlaceSelected;

    private bool _isChipSelect;

    private bool _isPlaceForChipSelect = true;
    private Chip _chipWithColor;

    // Update is called once per frame
    private void Update()
    {
        SelectChip();

       SelectPlaceForMoving();
    }

    private void SelectPlaceForMoving()
    {
        if (Input.GetMouseButtonDown(0) && !_isPlaceForChipSelect )
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo))
            {
                var chipWithoutColor = hitInfo.collider.GetComponent<Chip>();
                
                if (chipWithoutColor == null)
                {
                    ChangePlaceStateSelector();
                    ChangeStateSelector();
                    _chipWithColor.ResetOutline();
                    return;
                }
                var outline = chipWithoutColor.GetComponent<Outline>();
                var transparentMaterial = chipWithoutColor.GetComponent<IHasTransparentMaterial>();
                if (transparentMaterial != null && outline.OutlineWidth > 0 && chipWithoutColor.isActiveAndEnabled)
                {
                    Debug.Log("Place was chose");
                    _chipWithColor.ResetOutline();
                    PlaceForChipSelected?.Invoke(chipWithoutColor);
                    PlaceSelected?.Invoke();
                    ChangePlaceStateSelector();
                }
            }
        }
    }

    private void SelectChip()
    {
        if (Input.GetMouseButtonDown(0) && !_isChipSelect)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hitInfo))
            {
                _chipWithColor = hitInfo.collider.GetComponent<Chip>();
                if (_chipWithColor != null)
                {
                    Debug.Log("Object was chose");
                    _chipWithColor.SetOutline();
                    ChipSelected?.Invoke(_chipWithColor);
                   ChangeStateSelector();
                    ChangePlaceStateSelector();
                }
            }
            
        }
        
    }

    public void ResetChip()
    {
        _chipWithColor.ResetOutline();
        ChangeStateSelector();
    }
    public void ChangeStateSelector()
    {
        _isChipSelect = !_isChipSelect;
    }

    public void ChangePlaceStateSelector()
    {
        _isPlaceForChipSelect = !_isPlaceForChipSelect;
    }
}