using System;
using UnityEngine;


public class ChipSelector : MonoBehaviour
{
    public event Action<Chip > OnChipSelected;

    public event Action<Chip> OnPlaceForChipSelected;
    public event Action OnPlaceSelected;

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
                var outline = chipWithoutColor.GetComponent<Outline>();
                var transparentMaterial = chipWithoutColor.GetComponent<IHasTransparentMaterial>();
                if (transparentMaterial != null && outline.OutlineWidth > 0 && chipWithoutColor.isActiveAndEnabled)
                {
                    Debug.Log("Place was chose");
                    _chipWithColor.ResetOutline();
                    OnPlaceForChipSelected?.Invoke(chipWithoutColor);
                    OnPlaceSelected?.Invoke();
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
                    OnChipSelected?.Invoke(_chipWithColor);
                   ChangeStateSelector();
                    ChangePlaceStateSelector();
                }
            }
            
        }
        
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