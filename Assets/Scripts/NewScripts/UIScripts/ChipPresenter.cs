using System;
using System.Collections.Generic;
using NewScripts.Chip;
using UnityEngine;
using UnityEngine.UIElements;

namespace NewScripts.UIScripts
{
    public class ChipPresenter 
    {

        private readonly ChipView _chipView;

        public ChipPresenter(ChipView chipView)
        {
            _chipView = chipView;
        }
        
        public List <ChipModel> DisplayChips(List<Vector2> coordinatePoints, 
            List<int> initialPointLocation,
            List<Color> listColors, GameObject mainPanel)
        {
            var list = _chipView.DisplayChips(coordinatePoints,initialPointLocation,listColors, mainPanel);
            return list;
        }
        
    }
}
