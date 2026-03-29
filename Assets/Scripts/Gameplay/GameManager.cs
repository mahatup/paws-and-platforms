using Assets.Scripts.UI;
using System;
using Zenject;

namespace Assets.Scripts.Gameplay
{
    public class GameManager : IInitializable, IDisposable
    {
        private CatService _catService;
        private ViewService _viewService;

        public bool isKeyCollected = false;

        public static bool StaticSkipIntroNextLoad = false;

        public GameState State { get; private set; } = GameState.Intro;

        public event Action LevelCompleted;
        public event Action KeyNotCollected;
        public event Action KeyCollected;

        [Inject]
        public GameManager(
            CatService catService)
        {
            _catService = catService;
        }

        public void Initialize()
        {
            _catService.SpaceShipStepped += OnSpaceShipStepped;
            KeyCollected?.Invoke();

            if (StaticSkipIntroNextLoad)
            {
                StaticSkipIntroNextLoad = false;
                StartGame();
            }
            else
            {
                EnterIntroState();
            }
        }

        public void Dispose()
        {
            _catService.SpaceShipStepped -= OnSpaceShipStepped;
        }

        private void OnSpaceShipStepped()
        {
            if (isKeyCollected)
            {
                EnterGameOverState();
                LevelCompleted?.Invoke();
            }
            else
            {
                KeyNotCollected?.Invoke();
            }
        }


        public void EnterIntroState()
        {
            State = GameState.Intro;
        }

        public void StartGame()
        {
            State = GameState.Playing;
        }

        public void EnterGameOverState()
        {
            State = GameState.GameOver;
        }
    }
}