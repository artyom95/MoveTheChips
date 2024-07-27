using NewScripts.UIScripts;

namespace NewScripts.Presenters
{
    public class GameResultPanelPresenter 

    {
        private readonly GameResultPanelView _gameResultPanelView;

        public GameResultPanelPresenter(GameResultPanelView gameResultPanelView)
        {
            _gameResultPanelView = gameResultPanelView;
        }

        public void ShowWinScreen()
        {
            _gameResultPanelView.ShowWinScreen();
        }
        public void ShowLoseScreen()
        {
            _gameResultPanelView.ShowLoseScreen();
        }
    }
}