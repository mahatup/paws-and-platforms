using Assets.Scripts.Gameplay;
using Zenject;

namespace Assets.Scripts.UI
{
    public class StartViewPresenter : IInitializable, ITickable
    {
        private GameManager _gameManager;
        private ViewService _viewService;
        private InputService _inputService;

        private StartView _startView;
        private RestartView _restartView;
        private CoinCounterView _coinCounterView;
        private LivesView _livesView;

        private int _currentLine = 0;
        private bool _isActive = true;

        [Inject]
        public StartViewPresenter(
            ViewService viewService,
            GameManager gameManager,
            InputService inputService)
        {
            _viewService = viewService;
            _gameManager = gameManager;
            _inputService = inputService;
        }

        public void Initialize()
        {
            _startView = _viewService.GetView<StartView>();
            _startView.Show();
            _restartView = _viewService.GetView<RestartView>();
            _coinCounterView = _viewService.GetView<CoinCounterView>();
            _livesView = _viewService.GetView<LivesView>();

            _currentLine = 0;
            _isActive = true;

            ShowLore();
        }

        public void Tick()
        {
            if (!_isActive) return;
            if (_inputService.IsSkipPressed)
            {
                ShowNextLoreLine();
            }
        }

        private void ShowLore()
        {
            if (_currentLine < _startView.LoreLines.Count)
            {
                _startView.SetLoreText(_startView.LoreLines[_currentLine]);
            }
        }

        private void ShowNextLoreLine()
        {
            _currentLine++;
            if (_currentLine < _startView.LoreLines.Count)
            {
                ShowLore();
            }
            else
            {
                _isActive = false;

                _startView.Hide();
                _restartView.Show();
                _coinCounterView.Show();
                _livesView.Show();
                _gameManager.StartGame();
            }
        }
    }
}