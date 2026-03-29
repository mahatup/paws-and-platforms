using Assets.Scripts.Configs;
using Assets.Scripts.Gameplay;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using Zenject;

namespace Assets.Scripts.UI
{
    public class ReceiveKeyViewPresenter : IInitializable, IDisposable
    {
        private float _messageDuration;

        private CatService _catService;
        private ViewService _viewService;
        private DelayConfig _delayConfig;
        private GameManager _gameManager;

        private ReceiveKeyView _receiveKeyView;
        private CancellationTokenSource _cts;

        public event Action KeyCollected;

        [Inject]
        public ReceiveKeyViewPresenter(
            CatService catService,
            ConfigsService configService,
            ViewService viewService,
            GameManager gameManager)
        {
            _catService = catService;
            _delayConfig = configService.GetConfig<DelayConfig>();
            _viewService = viewService;
            _gameManager = gameManager;
        }

        public void Initialize()
        {
            _receiveKeyView = _viewService.GetView<ReceiveKeyView>();

            _cts = new CancellationTokenSource();
            _messageDuration = _delayConfig.ReceiveKeyView;

            _catService.KeyCollected += OnKeyCollected;
        }

        public void Dispose()
        {
            _catService.KeyCollected -= OnKeyCollected;

            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        }

        private void OnKeyCollected()
        {
            _gameManager.isKeyCollected = true;
            _receiveKeyView.Show();
            KeyCollected?.Invoke();
            HideAfterDelayAsync(_messageDuration, _cts.Token).Forget();
        }

        private async UniTaskVoid HideAfterDelayAsync(float delaySeconds, CancellationToken token)
        {
            await UniTask.Delay(
                TimeSpan.FromSeconds(delaySeconds),
                cancellationToken: token);

            _receiveKeyView.ClearText();
            _receiveKeyView.Hide();
        }
    }
}