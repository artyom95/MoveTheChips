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
            List<int> initialPointLocation,
            List<Color> listColors, GameObject mainPanel)
        {
            var mainChipList = _chipView.ShowChips(coordinatePoints, initialPointLocation, listColors, mainPanel);
            return mainChipList;
        }

        public List<ChipModelSettings> DisplayTargetChips(List<Vector2> coordinatePoints,
            List<int> finishPointLocation, List<int> initialPointLocation,
            List<Color> listColors, GameObject mainPanel)
        {
            var targetChipList = _chipView.ShowTargetChips(coordinatePoints,
                finishPointLocation,
                initialPointLocation,
                listColors,
                mainPanel);
            return targetChipList;
        }

        public void Clear()
        {
            _chipView.ClearChipLists();
        }
    }
}