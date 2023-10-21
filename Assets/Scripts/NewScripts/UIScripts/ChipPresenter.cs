using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace NewScripts.UIScripts
{
    public class ChipPresenter : MonoBehaviour
    {

        [SerializeField] private ChipView _chipView;
        
        public List <ChipModel> DisplayChips(List<Vector2> coordinatePoints, 
            List<int> initialPointLocation,
            List<Color> listColors, GameObject mainPanel)
        {
            var list = _chipView.DisplayChips(coordinatePoints,initialPointLocation,listColors, mainPanel);
            return list;
        }
        
    }
}
