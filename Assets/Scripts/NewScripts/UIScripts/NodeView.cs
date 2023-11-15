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

        public List<NodeModel> NodeModelsList { get;  } = new();

       
        public void ShowNodes(List<ChipModelSettings> listChips,
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
                NodeModelsList.Add(nodeModel);
                nodeModel.transform.SetParent(mainPanel.transform);
            }

            FillChipModel(listChips, initialPointLocation);
            FillNeighbours(connectionsBetweenPointPairs);
        }

        private void FillNeighbours(List<Vector2> connectionsBetweenPointPairs)
        {
            foreach (var connectionBetweenPointPair in connectionsBetweenPointPairs)
            {
                NodeModelsList[(int)(connectionBetweenPointPair.x - 1)]
                    .SetNeighbours(NodeModelsList[(int)(connectionBetweenPointPair.y - 1)]);

                NodeModelsList[(int)(connectionBetweenPointPair.y - 1)]
                    .SetNeighbours(NodeModelsList[(int)(connectionBetweenPointPair.x - 1)]);
            }
        }

        private void FillChipModel(List<ChipModelSettings> listChips, List<int> initialPointLocation)
        {
            for (var i = 0; i < initialPointLocation.Count; i++)
            {
                var nodeIndex = initialPointLocation[i];
                NodeModelsList[nodeIndex - 1].SetChipModel(listChips[i]);
            }
        }
    }
}