using System;
using System.Collections.Generic;
using UnityEngine;

namespace NewScripts.UIScripts
{
    public class GraphPresenter : MonoBehaviour
    {
        public event Action ShowBoardsEnded;

        [SerializeField] private GameObject _mainPanel;
        [SerializeField] private GameObject _secondPanel;
        [SerializeField] private Vector3 _newLineRendererPosition;
        [SerializeField] private Vector3 _newChipPosition;

        private GraphView _graphView;
        private ChipPresenter _chipPresenter;
        private NodePresenter _nodePresenter;
      
        private readonly Vector3 _secondPanelScale = new(0.5f, 0.5f, 0.5f);

        public GraphPresenter(GraphView graphView,
            ChipPresenter chipPresenter,
            NodePresenter nodePresenter)
        {
            _graphView = graphView;
            _chipPresenter = chipPresenter;
            _nodePresenter = nodePresenter;
        }

        public void Initialize(List<Vector2> coordinatesPoints,
            List<Vector2> connectionsBetweenPointPairs,
            List<int> initialPointLocation,
            List<Color> listColors,
            List<int> finishPointLocation)
        {
            ShowMainBoard(coordinatesPoints, connectionsBetweenPointPairs, initialPointLocation, listColors,
                finishPointLocation, _mainPanel);
            ShowSecondBoard(coordinatesPoints, connectionsBetweenPointPairs, _secondPanel, finishPointLocation,
                listColors, _newLineRendererPosition);

            _secondPanel.transform.localPosition = _newChipPosition;
            _secondPanel.transform.localScale = _secondPanelScale;
            ShowBoardsEnded?.Invoke();
        }

        private void ShowMainBoard(List<Vector2> coordinatesPoints, List<Vector2> connectionsBetweenPointPairs,
            List<int> initialPointLocation, List<Color> listColors, List<int> finishPointLocation, GameObject mainPanel)
        {
            _graphView.DisplayGraphs(coordinatesPoints, connectionsBetweenPointPairs, mainPanel);
            var listChips = _chipPresenter.DisplayChips(coordinatesPoints, initialPointLocation, listColors, mainPanel);
            _nodePresenter.DisplayNodes(listChips, coordinatesPoints, initialPointLocation,
                connectionsBetweenPointPairs,
                finishPointLocation, mainPanel);
        }

        private void ShowSecondBoard(List<Vector2> coordinatesPoints, List<Vector2> connectionsBetweenPointPairs,
            GameObject secondPanel, List<int> finishPointLocation, List<Color> listColors, Vector3 newPosition)
        {
            _graphView.DisplayGraphs(coordinatesPoints, connectionsBetweenPointPairs, secondPanel, newPosition);
            _chipPresenter.DisplayChips(coordinatesPoints, finishPointLocation, listColors, secondPanel);
        }
    }
}