using System.Collections.Generic;
using System.Threading.Tasks;
using NewScripts.Events;
using NewScripts.GameObjectsPresenter;
using NewScripts.UIScripts;
using UniTaskPubSub;
using UnityEngine;


public class GraphPresenter
{
    private readonly GameObject _mainPanel;
    private readonly GameObject _secondPanel;


    private readonly GraphView _graphView;
    private readonly ChipPresenter _chipPresenter;
    private readonly NodePresenter _nodePresenter;
    private readonly AsyncMessageBus _messageBus;

    public GraphPresenter(GraphView graphView,
        ChipPresenter chipPresenter,
        NodePresenter nodePresenter,
        PanelPresenter panelPresenterFactory,
        AsyncMessageBus messageBus)
    {
        _messageBus = messageBus;
        _graphView = graphView;
        _chipPresenter = chipPresenter;
        _nodePresenter = nodePresenter;
        _mainPanel = panelPresenterFactory.MainPanel;
        _secondPanel = panelPresenterFactory.SecondPanel;
    }

    public async Task Initialize(List<Vector2> coordinatesPoints,
        List<Vector2> connectionsBetweenPointPairs,
        List<int> initialPointLocation,
        List<Color> listColors,
        List<int> finishPointLocation)
    {
        ShowMainBoard(coordinatesPoints, connectionsBetweenPointPairs, initialPointLocation, listColors,
            finishPointLocation, _mainPanel);
        ShowSecondBoard(coordinatesPoints, connectionsBetweenPointPairs, _secondPanel, finishPointLocation,
            listColors);

        await _messageBus.PublishAsync(new ShowBoardEvent());
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
        GameObject secondPanel, List<int> finishPointLocation, List<Color> listColors)
    {
        _graphView.DisplayGraphs(coordinatesPoints, connectionsBetweenPointPairs, secondPanel);
        _chipPresenter.DisplayChips(coordinatesPoints, finishPointLocation, listColors, secondPanel);
    }
}