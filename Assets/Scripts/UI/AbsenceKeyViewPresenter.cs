using Assets.Scripts.Configs;
using Assets.Scripts.Gameplay;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using Zenject;

namespace Assets.Scripts.UI
{
    public class AbsenceKeyViewPresenter : IInitializable, IDisposable
    {
        private float _messageDuration;

        private GameManager _gameManager;
        private ViewService _viewService;
        private DelayConfig _delayConfig;

        private AbsenceKeyView _absenceKeyView;

        [Inject]
        public AbsenceKeyViewPresenter(
            GameManager gameManager,
            ConfigsService configService,
            ViewService viewService)
        {
            _gameManager = gameManager;
            _delayConfig = configService.GetConfig<DelayConfig>();
            _viewService = viewService;
        }

        public void Initialize()
        {
            _gameManager.KeyNotCollected += OnKeyNotCollected;
            _absenceKeyView = _viewService.GetView<AbsenceKeyView>();
            _messageDuration = _delayConfig.AbsenceKeyDelay;
        }

        public void Dispose()
        {
            _gameManager.KeyNotCollected -= OnKeyNotCollected;
        }

        private void OnKeyNotCollected()
        {
            ShowTemporaryMessageAsync(_absenceKeyView.GetCancellationTokenOnDestroy()).Forget();
        }

        private async UniTaskVoid ShowTemporaryMessageAsync(CancellationToken token)
        {
            _absenceKeyView.Show();

            await UniTask.Delay(TimeSpan.FromSeconds(_messageDuration), cancellationToken: token);

            _absenceKeyView.Hide();
        }
    }
}