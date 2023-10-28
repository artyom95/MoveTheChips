using System.Collections.Generic;
using NewScripts.Chip;
using NewScripts.Node;
using UnityEngine;

namespace NewScripts.UIScripts
{
    public class NodeView : MonoBehaviour
    {
        public List<int> FinishPointLocation { get; private set; } = new();

        [SerializeField] private NodeModel _nodeModel;

        private List<NodeModel> _nodeModelsList = new();

        public List<NodeModel> GetNodeModelList()
        {
            return _nodeModelsList;
        }
        public void DisplayNodes(List<ChipModel> listChips,
            List<Vector2> coordinatesPoints,
            List<int> initialPointLocation,
            List<Vector2> connectionsBetweenPointPairs,
            List<int> finishPointLocation, GameObject mainPanel)
        {
            FinishPointLocation = finishPointLocation;
            for (var i = 0; i < coordinatesPoints.Count; i++)
            {
                var nodeModel = Instantiate(_nodeModel);
                var position = new Vector3(coordinatesPoints[i].x, -coordinatesPoints[i].y, 0);
                nodeModel.transform.position = position;
                nodeModel.SetPosition(position);
                nodeModel.SetID(i + 1);
                _nodeModelsList.Add(nodeModel);
                nodeModel.transform.SetParent(mainPanel.transform);
            }

            FillChipModel(listChips, initialPointLocation);
            FillNeighbours(connectionsBetweenPointPairs);
        }

        private void FillNeighbours(List<Vector2> connectionsBetweenPointPairs)
        {
            foreach (var connectionBetweenPointPair in connectionsBetweenPointPairs)
            {
                _nodeModelsList[(int)(connectionBetweenPointPair.x - 1)]
                    .SetNeighbours(_nodeModelsList[(int)(connectionBetweenPointPair.y - 1)]);

                _nodeModelsList[(int)(connectionBetweenPointPair.y - 1)]
                    .SetNeighbours(_nodeModelsList[(int)(connectionBetweenPointPair.x - 1)]);
            }
        }

        private void FillChipModel(List<ChipModel> listChips, List<int> initialPointLocation)
        {
            for (var i = 0; i < initialPointLocation.Count; i++)
            {
                var nodeIndex = initialPointLocation[i];
                _nodeModelsList[nodeIndex - 1].SetChipModel(listChips[i]);
            }
        }
    }
}