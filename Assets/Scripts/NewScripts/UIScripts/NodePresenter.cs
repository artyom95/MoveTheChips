using System.Collections.Generic;
using UnityEngine;

namespace NewScripts.UIScripts
{
    public class NodePresenter : MonoBehaviour
    { 
        
        private NodeView _nodeView;

        public NodePresenter(NodeView nodeView)
        {
            _nodeView = nodeView;
        }
        public void DisplayNodes(List<ChipModel> listChips, 
            List<Vector2> coordinatesPoints, 
            List<int> initialPointLocation,
            List<Vector2> connectionsBetweenPointPairs,
            List<int> finishPointLocation, GameObject mainPanel)
        {
            _nodeView.DisplayNodes(listChips, 
                coordinatesPoints, 
                initialPointLocation,
                connectionsBetweenPointPairs,
                finishPointLocation, mainPanel);
        }
    }
}
