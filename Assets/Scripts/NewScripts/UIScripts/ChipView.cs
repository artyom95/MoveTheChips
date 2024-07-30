using System.Collections.Generic;
using NewScripts.Chip;
using UnityEngine;

namespace NewScripts.UIScripts
{
    public class ChipView : MonoBehaviour
    {
        [SerializeField] private ChipModelSettings _chipModel;
        private readonly List<ChipModelSettings> _chipsList = new();
        private List<ChipModelSettings> _listMainChipModels = new();
        private List<ChipModelSettings> _listTargetChipModels = new();

        public List<ChipModelSettings> ShowChips(List<Vector2> coordinatePoints,
            List<int> pointLocations, List<Color> colors, GameObject mainPanel,
            bool isTargetChips = false, List<int> initialPointLocation = null)
        {
            var sequenceNumberCycle = 0;

            foreach (var point in pointLocations)
            {
                if (point == 0)
                {
                    sequenceNumberCycle++;
                    continue;
                }

                var chipPosition = GetChipPosition(coordinatePoints, point, sequenceNumberCycle, isTargetChips);
                var chipModel = InstantiateChipModel(chipPosition, point, colors, sequenceNumberCycle, mainPanel,
                    isTargetChips, initialPointLocation);

                if (isTargetChips)
                {
                    _listTargetChipModels.Add(chipModel);
                }
                else
                {
                    _listMainChipModels.Add(chipModel);
                }

                sequenceNumberCycle++;
            }

            return _chipsList;
        }

        private Vector3 GetChipPosition(List<Vector2> coordinatePoints, int point, int sequenceNumberCycle,
            bool isTargetChips)
        {
            var xPosition = 0f;
            var yPosition = 0f;
            if (isTargetChips)
            {
                xPosition = coordinatePoints[sequenceNumberCycle].x;
                yPosition = -coordinatePoints[sequenceNumberCycle].y;
            }
            else
            {
                xPosition = coordinatePoints[point - 1].x;
                yPosition = -coordinatePoints[point - 1].y;
            }


            return new Vector3(xPosition, yPosition, 0);
        }

        private ChipModelSettings InstantiateChipModel(Vector3 chipPosition, int point, List<Color> colors,
            int sequenceNumberCycle, GameObject mainPanel, bool isTargetChips,
            List<int> initialPointLocation)
        {
            var chipModel = Instantiate(_chipModel, mainPanel.transform, true);
            chipModel.SetPosition(chipPosition);
            chipModel.SetID(point);

            int colorIndex;

            if (isTargetChips && initialPointLocation != null)
            {
                colorIndex = initialPointLocation.IndexOf(point);
            }
            else
            {
                colorIndex = sequenceNumberCycle;
            }

            chipModel.SetColor(colors[colorIndex]);
            _chipsList.Add(chipModel);

            Transform transform1 = chipModel.transform;
            transform1.localPosition = chipPosition;
            ApplyScale(transform1, mainPanel.transform.localScale);

            return chipModel;
        }

        private void ApplyScale(Transform transform1, Vector3 localScale)
        {
            var scale = transform1.localScale;
            transform1.localScale = new Vector3(
                scale.x * localScale.x,
                scale.y * localScale.y,
                scale.z * localScale.z);
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