using Assets.Scripts.Gameplay;
using System;
using Zenject;

namespace Assets.Scripts.UI
{
    public class GameOverViewPresenter : IInitializable, IDisposable
    {
        private GameManager _gameManager;
        private ViewService _viewService;

        private GameOverView _gameOverView;

        public GameOverViewPresenter(
        GameManager gameManager,
        ViewService viewService)
        {
            _gameManager = gameManager;
            _viewService = viewService;
        }

        public void Initialize()
        {
            _gameOverView = _viewService.GetView<GameOverView>();

            _gameManager.LevelCompleted += OnLevelCompleted;
        }

        public void Dispose()
        {
            _gameManager.LevelCompleted -= OnLevelCompleted;
        }

        private void OnLevelCompleted()
        {
            _gameOverView.Show();
        }
    }
}