using UnityEngine;

namespace NewScripts.GameObjectsPresenter
{
    public class PanelPresenter
    {
        public GameObject MainPanel { get; }
        public GameObject SecondPanel { get; }
        public GameObject WinPanel { get; }
        public GameObject LosePanel { get; }

        public PanelPresenter(GameObject mainPanel, GameObject secondPanel, GameObject winPanel,
            GameObject losePanel)
        {
            MainPanel = mainPanel;
            SecondPanel = secondPanel;
            WinPanel = winPanel;
            LosePanel = losePanel;
        }
    }
}