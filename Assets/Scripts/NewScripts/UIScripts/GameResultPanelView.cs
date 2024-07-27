using UnityEngine;

namespace NewScripts.UIScripts
{
    public class GameResultPanelView
    {
        private GameObject _winPanel { get; }
        private GameObject _losePanel { get; }

        public GameResultPanelView(PanelProvider panelProviderFactory)
        {
            _winPanel = panelProviderFactory.WinPanel;
            _losePanel = panelProviderFactory.LosePanel;
        }

        public void ShowWinScreen()
        {
            _winPanel.gameObject.SetActive(true);
        }

        public void ShowLoseScreen()
        {
            _losePanel.gameObject.SetActive(true);
        }

        public void HideLoseScreen()
        {
            _losePanel.SetActive(false);
        }
    }
}