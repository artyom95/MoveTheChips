using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NewScripts.Chip;
using NewScripts.UIScripts;
using UniTaskPubSub;
using UnityEngine;

namespace NewScripts.Presenters
{
    public class NodePresenter

    {
        private readonly NodeView _nodeView;
        private IDisposable _subscription;
        private readonly AsyncMessageBus _messageBus;

        public NodePresenter(NodeView nodeView,
            AsyncMessageBus messageBus)
        {
            _messageBus = messageBus;
            _nodeView = nodeView;
        }

        public async Task DisplayNodes(List<ChipModelSettings> listChips,
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


            await _messageBus.PublishAsync(new FindFinishPointsLocationEvent(_nodeView.FinishPointLocation));
            await _messageBus.PublishAsync(new FindNodeModelsListEvent(_nodeView.NodeModelsList));
        }

        public void ClearNodes()
        {
            _nodeView.ClearListNodes();
        }
    }
}