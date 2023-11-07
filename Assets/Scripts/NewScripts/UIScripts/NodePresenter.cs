using System.Collections.Generic;
using NewScripts.Chip;
using NewScripts.Node;
using UnityEngine;

namespace NewScripts.UIScripts
{
    public class NodePresenter
    {
        private readonly NodeView _nodeView;

        public NodePresenter(NodeView nodeView)
        {
            _nodeView = nodeView;
        }

        public void DisplayNodes(List<ChipModelSettings> listChips,
            List<Vector2> coordinatesPoints,
            List<int> initialPointLocation,
            List<Vector2> connectionsBetweenPointPairs,
            List<int> finishPointLocation, GameObject mainPanel)
        {
            _nodeView.ShowNodes(listChips,
                coordinatesPoints,
                initialPointLocation,
                connectionsBetweenPointPairs,
                finishPointLocation, mainPanel);
        }

        public List<int> GetFinishPointLocation()
        {
            return _nodeView.FinishPointLocation;
        }
        public List<NodeModel> GetNodeModelList()
        {
            return _nodeView.GetNodeModelList();
        }
    }
}