using UnityEngine;

namespace NewScripts
{
    public class PanelProvider
    {
        public GameObject MainPanel { get; }
        public GameObject SecondPanel { get; }
        public GameObject WinPanel { get; }
        public GameObject LosePanel { get; }

        public PanelProvider(GameObject mainPanel, GameObject secondPanel, GameObject winPanel,
            GameObject losePanel)
        {
            MainPanel = mainPanel;
            SecondPanel = secondPanel;
            WinPanel = winPanel;
            LosePanel = losePanel;
        }
    }
}