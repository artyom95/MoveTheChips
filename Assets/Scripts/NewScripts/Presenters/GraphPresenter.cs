using System.Collections.Generic;
using System.Threading.Tasks;
using NewScripts.Chip;
using NewScripts.Events;
using NewScripts.UIScripts;
using UniTaskPubSub;
using UnityEngine;

namespace NewScripts.Presenters
{
    public class GraphPresenter
    {
        private readonly GameObject _mainPanel;
        private readonly GameObject _secondPanel;
        private readonly GraphView _graphView;
        private readonly ChipPresenter _chipPresenter;
        private readonly NodePresenter _nodePresenter;
        private readonly AsyncMessageBus _messageBus;
        private List<ChipModelSettings> _listMainChips;

        public GraphPresenter(GraphView graphView,
            ChipPresenter chipPresenter,
            NodePresenter nodePresenter,
            PanelProvider panelProviderFactory,
            AsyncMessageBus messageBus)
        {
            _messageBus = messageBus;
            _graphView = graphView;
            _chipPresenter = chipPresenter;
            _nodePresenter = nodePresenter;
            _mainPanel = panelProviderFactory.MainPanel;
            _secondPanel = panelProviderFactory.SecondPanel;
        }

        public async Task Initialize(List<Vector2> coordinatesPoints,
            List<Vector2> connectionsBetweenPointPairs,
            List<int> initialPointLocation,
            List<Color> listColors,
            List<int> finishPointLocation)
        {
            await ShowMainBoard(coordinatesPoints, connectionsBetweenPointPairs, initialPointLocation, listColors,
                finishPointLocation, _mainPanel);
            ShowSecondBoard(coordinatesPoints, connectionsBetweenPointPairs, _secondPanel, finishPointLocation,
                initialPointLocation,
                listColors);

            await _messageBus.PublishAsync(new ShowBoardEvent());
        }

        public void ClearGraph()
        {
            _graphView.Clear();
            _chipPresenter.Clear();
            _nodePresenter.ClearNodes();
        }

        private async Task ShowMainBoard(List<Vector2> coordinatesPoints, List<Vector2> connectionsBetweenPointPairs,
            List<int> pointLocation, List<Color> listColors, List<int> finishPointLocation, GameObject mainPanel)
        {
            _graphView.DisplayGraphs(coordinatesPoints, connectionsBetweenPointPairs, mainPanel, "main");

            _listMainChips = _chipPresenter.DisplayChips(coordinatesPoints, pointLocation, listColors, mainPanel);

            await _nodePresenter.DisplayNodes(_listMainChips, coordinatesPoints, pointLocation,
                connectionsBetweenPointPairs,
                finishPointLocation, mainPanel);
        }

        private void ShowSecondBoard(List<Vector2> coordinatesPoints, List<Vector2> connectionsBetweenPointPairs,
            GameObject secondPanel, List<int> finishPointLocation, List<int> initialPointLocation, List<Color> listColors)
        {
            _graphView.DisplayGraphs(coordinatesPoints, connectionsBetweenPointPairs, secondPanel, "target");

            _chipPresenter.DisplayChips(coordinatesPoints, finishPointLocation, listColors, secondPanel, true,
                initialPointLocation);
        }
    }
}