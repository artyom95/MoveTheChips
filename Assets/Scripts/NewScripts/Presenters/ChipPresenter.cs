using System.Collections.Generic;
using NewScripts.Chip;
using NewScripts.UIScripts;
using UnityEngine;

namespace NewScripts.Presenters
{
    public class ChipPresenter
    {
        private readonly ChipView _chipView;

        public ChipPresenter(ChipView chipView)
        {
            _chipView = chipView;
        }

        public List<ChipModelSettings> DisplayChips(List<Vector2> coordinatePoints,
            List<int> pointLocation,
            List<Color> listColors, GameObject rootPanel, bool isTargetChip = false,
            List<int> initialPointLocation = null)
        {
            var chipList = new List<ChipModelSettings>();
            if (!isTargetChip && initialPointLocation == null)
            {
                chipList = _chipView.ShowChips(coordinatePoints, pointLocation, listColors, rootPanel);
            }
            else
            {
                chipList = _chipView.ShowChips(coordinatePoints, pointLocation, listColors, rootPanel, isTargetChip,
                    initialPointLocation);
            }

            return chipList;
        }

        public void Clear()
        {
            _chipView.ClearChipLists();
        }
    }
}