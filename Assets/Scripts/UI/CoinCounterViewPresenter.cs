using Assets.Scripts.Gameplay;
using System;
using Zenject;

namespace Assets.Scripts.UI
{
    public class CoinCounterViewPresenter : IInitializable, IDisposable
    {
        private CatService _catService;
        private ViewService _viewService;

        private CoinCounterView _coinCounterView;

        private int _coinCounter;

        [Inject]
        public CoinCounterViewPresenter(
            CatService catService,
            ViewService viewService)
        {
            _catService = catService;
            _viewService = viewService;
        }

        public void Initialize()
        {
            _coinCounterView = _viewService.GetView<CoinCounterView>();

            _catService.CoinCollected += OnCoinCollected;
        }

        public void Dispose()
        {
            _catService.CoinCollected -= OnCoinCollected;
        }

        private void OnCoinCollected()
        {
            _coinCounter++;
            _coinCounterView.SetCounterCoin(_coinCounter);
        }
    }
}