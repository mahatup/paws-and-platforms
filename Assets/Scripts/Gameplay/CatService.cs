using Assets.Scripts.Configs;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay
{
    public class CatService : MonoBehaviour
    {
        [SerializeField] private Cat _cat;

        [SerializeField] private Camera _camera;

        private bool _canMove = false;

        private ConfigsService _configService;
        private CatKnockbackConfig _catKnockbackConfig;
        private CatConfig _catConfig;

        private InputService _inputService;
        private GameManager _gameManager;

        private CatHealthService _healthService;
        private CatMovementService _movementService;
        private CatKnockbackService _knockbackService;

        public event Action HeartDropped;
        public event Action<int> HeartSpawned;
        public event Action CoinCollected;
        public event Action KeyCollected;
        public event Action SpaceShipStepped;
        public event Action Dead;

        [Inject]
        public void Construct(
            InputService inputService,
            DiContainer container,
            GameManager gameManager,
            ConfigsService configService)
        {
            _inputService = inputService;
            _gameManager = gameManager;
            _configService = configService;
            _catConfig = configService.GetConfig<CatConfig>();
            _catKnockbackConfig = configService.GetConfig<CatKnockbackConfig>();
        }

        private void Start()
        {
            _camera.CameraReady += OnCameraReady;
            _camera.Construct(_cat);

            _healthService = new CatHealthService(_catConfig.Lives);

            _movementService = new CatMovementService(_cat, _configService, _inputService);

            _knockbackService = new CatKnockbackService(_catKnockbackConfig);

            _healthService.HeartDropped += OnHeartDropped;
            _healthService.HeartSpawned += OnHeartSpawned;
            _healthService.Dead += OnDead;

            _movementService.AirDeath += OnDead;

            _healthService.Init();

            _cat.CoinCollected += OnCoinCollected;
            _cat.KeyCollected += OnKeyCollected;
            _cat.SpaceShipStepped += OnSpaceShipStepped;
            _cat.TrapStepped += OnCatKnocked;

            EventManager.CatKnocked += OnCatKnocked;
            EventManager.EnemyKilled += OnEnemyKilled;
        }


        private void OnDisable()
        {
            _camera.CameraReady -= OnCameraReady;

            EventManager.CatKnocked -= OnCatKnocked;
            EventManager.EnemyKilled -= OnEnemyKilled;

            _healthService.HeartDropped -= OnHeartDropped;
            _healthService.HeartSpawned -= OnHeartSpawned;
            _healthService.Dead -= OnDead;

            _movementService.AirDeath -= OnDead;

            _cat.CoinCollected -= OnCoinCollected;
            _cat.KeyCollected -= OnKeyCollected;
            _cat.SpaceShipStepped -= OnSpaceShipStepped;
            _cat.TrapStepped -= OnCatKnocked;

        }


        private void OnCameraReady() => _canMove = true;
        private void OnHeartDropped() => HeartDropped?.Invoke();
        private void OnHeartSpawned(int count) => HeartSpawned?.Invoke(count);
        private void OnCoinCollected() => CoinCollected?.Invoke();
        private void OnKeyCollected() => KeyCollected?.Invoke();
        private void OnSpaceShipStepped() => SpaceShipStepped?.Invoke();
        private void OnDead() => Dead?.Invoke();

        //TODO: разобраться с гейм менеджером
        private void Update()
        {
            if (!_canMove || _gameManager.State != GameState.Playing)
            {
                return;
            }

            _movementService.Update();
        }

        private void OnEnemyKilled(IDamager damager)
        {
            _knockbackService.ApplyKnockback(_cat, damager.Position2D);
        }

        private void OnCatKnocked(IDamager damager)
        {
            _knockbackService.ApplyKnockback(_cat, damager.Position2D);
            _healthService.DecreaseHealth();
        }

    }
}