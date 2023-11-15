using System.Collections.Generic;
using System.Threading.Tasks;
using NewScripts.Events;
using NewScripts.GameObjectsPresenter;
using NewScripts.UIScripts;
using UniTaskPubSub;
using UnityEngine;


public class GraphPresenter
{
    private GameObject _mainPanel;
    private GameObject _secondPanel;


    private GraphView _graphView;
    private ChipPresenter _chipPresenter;
    private NodePresenter _nodePresenter;

    /// <summary>
    /// It must work another way
    /// </summary>
    private readonly Vector3 _secondPanelScale = new(0.25f, 0.25f, 0.25f);

    private readonly Vector3 _position = new(378, -53, 0);
    private readonly Vector3 _newChipPosition = new(480, 0, 0);
    private AsyncMessageBus _messageBus;

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
            listColors, _newChipPosition);

        _secondPanel.transform.localPosition = _position;

        _secondPanel.transform.localScale = _secondPanelScale;

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
        GameObject secondPanel, List<int> finishPointLocation, List<Color> listColors, Vector3 newPosition)
    {
        _graphView.DisplayGraphs(coordinatesPoints, connectionsBetweenPointPairs, secondPanel, newPosition);
        _chipPresenter.DisplayChips(coordinatesPoints, finishPointLocation, listColors, secondPanel);
    }
}