using System.Collections.Generic;
using NewScripts.Chip;
using UnityEngine;

namespace NewScripts.UIScripts
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
            var list = _chipView.ShowChips(coordinatePoints, initialPointLocation, listColors, mainPanel);
            return list;
        }
    }
}