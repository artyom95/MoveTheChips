using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace NewScripts.UIScripts
{
    public class NodePresenter : MonoBehaviour
    {
        [SerializeField] private NodeView _nodeView;

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
