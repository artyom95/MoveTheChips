using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelPresenterFactory
{
    public GameObject MainPanel { get; }
    public GameObject SecondPanel { get; }
    public GameObject WinPanel { get; }
    public GameObject LosePanel { get; }

    public PanelPresenterFactory(GameObject mainPanel, GameObject secondPanel, GameObject winPanel,
        GameObject losePanel)
    {
        MainPanel = mainPanel;
        SecondPanel = secondPanel;
        WinPanel = winPanel;
        LosePanel = losePanel;
    }
}