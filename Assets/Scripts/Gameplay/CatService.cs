using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;

public enum EStates
{
    Idle,
    Run,
    Jump
}
public class CatService : MonoBehaviour
{
    [SerializeField] private Cat _cat;
    [SerializeField] private CatConfig _catConfig;
    [SerializeField] private CatKnockbackConfig _catKnockbackConfig;
    [SerializeField] private InputConfig _inputConfig;
    [SerializeField] private Camera _camera;

    private bool _canMove = false;

    private InputService _inputService;
    private CatHealthService _healthService;
    private CatMovementService _movementService;
    private CatTriggerService _catTriggerService;
    private CatKnockbackService _knockbackService;

    public event Action HeartDropped;
    public event Action<int> HeartSpawned;
    public event Action CoinCollected;
    public event Action KeyCollected;
    public event Action SpaceShipStepped;
    public event Action Dead;

    private void Awake()
    {
        _inputService = new InputService(_inputConfig);
        _healthService = new CatHealthService(_catConfig.Lives);
        _movementService = new CatMovementService(_cat, _catConfig, _inputService);
        _catTriggerService = new CatTriggerService();
        _knockbackService = new CatKnockbackService(_cat, _catKnockbackConfig);

        _healthService.HeartDropped += OnHeartDropped;
        _healthService.HeartSpawned += OnHeartSpawned;
        _healthService.Dead += OnDead;

        _movementService.AirDeath += OnDead;

        _catTriggerService.CoinCollected += OnCoinCollected;
        _catTriggerService.KeyCollected += OnKeyCollected;
        _catTriggerService.SpaceShipStepped += OnSpaceShipStepped;

        _healthService.Init();
    }

    private void OnEnable()
    {
        EventManager.CatKnocked += OnCatKnocked;
        EventManager.EnemyKilled += OnEnemyKilled;
    }

    private void Start()
    {
        _camera.CameraReady += OnCameraReady;
        _camera.Construct(_cat);
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

        _catTriggerService.CoinCollected -= OnCoinCollected;
        _catTriggerService.KeyCollected -= OnKeyCollected;
        _catTriggerService.SpaceShipStepped -= OnSpaceShipStepped;

        
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
        if (!_canMove || GameManager.Instance.State != GameState.Playing)
        {
            return;
        }

        _movementService.Update();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _catTriggerService.SetCollision(collision);
    }

    private void OnCatKnocked(Vector2 sourcePosition, Vector2 sourceVelocity)
    {
        _knockbackService.ApplyKnockback(sourcePosition, sourceVelocity);
        _healthService.DecreaseHealth();
    }

    private void OnEnemyKilled(Vector2 sourcePosition, Vector2 sourceVelocity)
    {
        _knockbackService.ApplyKnockback(sourcePosition, sourceVelocity);
    }
}
