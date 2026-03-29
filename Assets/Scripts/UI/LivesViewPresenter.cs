using Assets.Scripts.Gameplay;
using System;
using Zenject;

namespace Assets.Scripts.UI
{
    public class LivesViewPresenter : IInitializable, IDisposable
    {
        private CatService _catService;

        private ViewService _viewService;
        private LivesView _livesView;

        [Inject]
        public LivesViewPresenter(
        CatService catService,
        ViewService viewService)
        {
            _catService = catService;
            _viewService = viewService;
        }

        public void Initialize()
        {
            _livesView = _viewService.GetView<LivesView>();

            _catService.HeartSpawned += OnHeartSpawned;
            _catService.HeartDropped += OnHeartDropped;
        }

        public void Dispose()
        {
            _catService.HeartSpawned -= OnHeartSpawned;
            _catService.HeartDropped -= OnHeartDropped; ;
        }

        private void OnHeartSpawned(int lives)
        {
            _livesView.Initialize(lives);
        }

        private void OnHeartDropped()
        {
            _livesView?.BreakLastHeart();
        }

    }
}