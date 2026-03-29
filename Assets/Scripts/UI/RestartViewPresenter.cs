using Assets.Scripts.Configs;
using Assets.Scripts.Gameplay;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine.SceneManagement;
using Zenject;

namespace Assets.Scripts.UI
{
    public class RestartViewPresenter : IInitializable, IDisposable
    {
        private float _delay;
        private CatService _catService;
        private ViewService _viewService;
        private RestartView _restartView;
        private DelayConfig _delayConfig;

        private CancellationTokenSource _restartSceneCts;

        [Inject]
        public RestartViewPresenter(
            CatService catService,
            ConfigsService configService,
            ViewService viewService)
        {
            _catService = catService;
            _delayConfig = configService.GetConfig<DelayConfig>();
            _viewService = viewService;
        }

        public void Initialize()
        {
            _restartView = _viewService.GetView<RestartView>();

            _restartView.RestartGameButtonClicked += OnRestartGameButtonClicked;
            _catService.Dead += Restart;
            _delay = _delayConfig.RestartViewDelay;
        }

        public void Dispose()
        {
            _restartView.RestartGameButtonClicked -= OnRestartGameButtonClicked;
            _catService.Dead -= Restart;

            _restartSceneCts?.Cancel();
            _restartSceneCts?.Dispose();
            _restartSceneCts = null;
        }

        private void OnRestartGameButtonClicked()
        {
            Restart();
        }

        private void Restart()
        {
            _restartSceneCts = new CancellationTokenSource();
            RestartAfterDelayAsync(_restartSceneCts.Token).Forget();
        }

        private async UniTaskVoid RestartAfterDelayAsync(CancellationToken token)
        {
            _restartView.Show();

            await UniTask.Delay(TimeSpan.FromSeconds(_delay), cancellationToken: token);

            GameManager.StaticSkipIntroNextLoad = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}