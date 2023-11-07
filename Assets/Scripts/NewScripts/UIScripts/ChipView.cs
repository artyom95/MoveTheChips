using System.Collections.Generic;
using NewScripts.Chip;
using UnityEngine;
using UnityEngine.UIElements;

namespace NewScripts.UIScripts
{
    public class ChipView : MonoBehaviour
    {
        [SerializeField] private ChipModelSettings _chipModel;
        private List<ChipModelSettings> _chipsList = new();

        public List<ChipModelSettings> ShowChips(List<Vector2> coordinatePoints,
            List<int> initialPointLocation,
            List<Color> colors, GameObject mainPanel)
        {
            var chipPosition = new Vector3();
            var sequenceNumberCycle = 0;
            foreach (var initialPoint in initialPointLocation)
            {
                if (initialPoint ==0)
                {
                    continue;
                }
                var xPosition = coordinatePoints[initialPoint - 1].x;
                var yPosition = -coordinatePoints[initialPoint - 1].y;

                chipPosition = new Vector3(xPosition, yPosition, 0);

                var chipModel = Instantiate(_chipModel);
                chipModel.transform.position = chipPosition;
                chipModel.SetPosition(chipPosition);
                chipModel.SetID(initialPoint);
                chipModel.SetColor(colors[sequenceNumberCycle]);
                _chipsList.Add(chipModel);
                chipModel.transform.SetParent(mainPanel.transform);
                sequenceNumberCycle++;
            }

            return _chipsList;
        }
    }
}