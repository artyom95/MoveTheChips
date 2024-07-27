using System.Collections.Generic;
using NewScripts.Chip;
using UnityEngine;

namespace NewScripts.UIScripts
{
    public class ChipView : MonoBehaviour
    {
        [SerializeField] private ChipModelSettings _chipModel;
        private readonly List<ChipModelSettings> _chipsList = new();
        private List<ChipModelSettings> _listMainChipModels = new ();
        private List<ChipModelSettings> _listTargetChipModels = new ();

        public List<ChipModelSettings> ShowChips(List<Vector2> coordinatePoints,
            List<int> initialPointLocation,
            List<Color> colors, GameObject mainPanel)
        {
            var chipPosition = new Vector3();
            var sequenceNumberCycle = 0;
            foreach (var initialPoint in initialPointLocation)
            {
                if (initialPoint == 0)
                {
                    sequenceNumberCycle++;
                    continue;
                }

                var xPosition = coordinatePoints[initialPoint - 1].x;
                var yPosition = -coordinatePoints[initialPoint - 1].y;

                chipPosition = new Vector3(xPosition, yPosition, 0);

                var chipModel = Instantiate(_chipModel, mainPanel.transform, true);
                chipModel.SetPosition(chipPosition);
                chipModel.SetID(initialPoint);
                chipModel.SetColor(colors[sequenceNumberCycle]);
                _chipsList.Add(chipModel);

                var transform1 = chipModel.transform;
                transform1.localPosition = chipPosition;
                var localScale = mainPanel.transform.localScale;
                var scale = transform1.localScale;
                scale = new Vector3(
                    scale.x * localScale.x,
                    scale.y * localScale.y,
                    scale.z * localScale.z);
                transform1.localScale = scale;
                _listMainChipModels.Add(chipModel);
                sequenceNumberCycle++;
            }

            return _chipsList;
        }

        public List<ChipModelSettings> ShowTargetChips(List<Vector2> coordinatePoints,
            List<int> finalPointLocation, List<int> initialPointLocation,
            List<Color> colors, GameObject mainPanel)
        {
            var chipPosition = new Vector3();
            var sequenceNumberCycle = 0;
            foreach (var initialPoint in finalPointLocation)
            {
                if (initialPoint == 0)
                {
                    sequenceNumberCycle++;
                    continue;
                }

                var xPosition = coordinatePoints[sequenceNumberCycle].x;
                var yPosition = -coordinatePoints[sequenceNumberCycle].y;

                chipPosition = new Vector3(xPosition, yPosition, 0);

                var chipModel = Instantiate(_chipModel, mainPanel.transform, true);
                chipModel.SetPosition(chipPosition);
                chipModel.SetID(initialPoint);
                var index = initialPointLocation.IndexOf(initialPoint);
                chipModel.SetColor(colors[index]);
                _chipsList.Add(chipModel);

                var transform1 = chipModel.transform;
                transform1.localPosition = chipPosition;
                var localScale = mainPanel.transform.localScale;
                var scale = transform1.localScale;
                scale = new Vector3(
                    scale.x * localScale.x,
                    scale.y * localScale.y,
                    scale.z * localScale.z);
                transform1.localScale = scale;

                _listTargetChipModels.Add(chipModel);

                sequenceNumberCycle++;
            }

            return _chipsList;
        }

        public void ClearChipLists()
        {
            ClearList(_listMainChipModels);
            ClearList(_listTargetChipModels);
            ClearList(_chipsList);
        }
        private void ClearList(List<ChipModelSettings> list)
        {
            foreach (var chipModelSettings in list)
            {
               Destroy(chipModelSettings.gameObject);
            }
            list.Clear();
        }
    }
}