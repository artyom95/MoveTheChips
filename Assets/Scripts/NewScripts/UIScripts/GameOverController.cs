using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameOverController
{
    private GameObject _winPanel { get; }
    private GameObject _losePanel { get; }

    public GameOverController(PanelPresenterFactory panelPresenterFactory)
    {
        _winPanel = panelPresenterFactory.WinPanel;
        _losePanel = panelPresenterFactory.LosePanel;
    }

    public void ShowWinScreen()
    {
        _winPanel.gameObject.SetActive(true);
    }

    public void ShowLoseScreen()
    {
        _losePanel.gameObject.SetActive(true);
    }
}