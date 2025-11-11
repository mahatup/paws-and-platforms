using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
    [SerializeField] private CatConfig _config;

    private CatHealthService _healthService;
    private CatMovementService _movementService;
    private CatTriggerService _catTriggerService;
    private CatKnockbackService _knockbackService;

    private float _airTime = 0f;

    public event Action HeartDropped;
    public event Action<int> HeartSpawned;
    public event Action CoinCollected;
    public event Action KeyCollected;
    public event Action SpaceShipStepped;
    public event Action Dead;

    private void Awake()
    {
        _healthService = new CatHealthService(_config.Lives);
        _movementService = new CatMovementService(_cat, _config);
        _catTriggerService = new CatTriggerService();
        _knockbackService = new CatKnockbackService(_cat);

        _healthService.HeartDropped += OnHeartDropped;
        _healthService.HeartSpawned += OnHeartSpawned;
        _healthService.Dead += OnDead;

        _catTriggerService.CoinCollected += OnCoinCollected;
        _catTriggerService.KeyCollected += OnKeyCollected;
        _catTriggerService.SpaceShipStepped += OnSpaceShipStepped;
    }

    private void OnEnable()
    {
        EventManager.CatKnocked += OnCatKnocked;
    }

    private void OnDisable()
    {
        EventManager.CatKnocked -= OnCatKnocked;

        _healthService.HeartDropped -= HeartDropped;
        _healthService.HeartSpawned -= HeartSpawned;
        _healthService.Dead -= OnDead;

        _catTriggerService.CoinCollected -= OnCoinCollected;
        _catTriggerService.KeyCollected -= OnKeyCollected;
        _catTriggerService.SpaceShipStepped += OnSpaceShipStepped;
    }

    private void OnHeartDropped() => HeartDropped?.Invoke();
    private void OnHeartSpawned(int count) => HeartSpawned?.Invoke(count);
    private void OnCoinCollected() => CoinCollected?.Invoke();
    private void OnKeyCollected() => KeyCollected?.Invoke();
    private void OnSpaceShipStepped() => SpaceShipStepped?.Invoke();

    private void Start()
    {
        _healthService.Init();
    }

    private void Update()
    {
        if (GameManager.Instance.State != GameState.Playing)
        {
            return;
        }

        _movementService.HandleInput();
        HandleAirTimeDeath();
    }
    private void HandleAirTimeDeath()
    {
        bool isGrounded = _movementService.IsGroundedPublic(); 

        if (!isGrounded)
        {
            _airTime += Time.deltaTime;
            if (_airTime >= _config.MaxAirTime)
            {
                
                Dead?.Invoke();
                _airTime = 0;
            }
        }
        else
        {
            _airTime = 0;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        _catTriggerService.HandleTrigger(collision);
    }

    private void OnCatKnocked()
    {
        _knockbackService.ApplyKnockback(transform.position);
        _healthService.TakeHeart();
    }
    
    private void OnDead()
    {
        Dead?.Invoke(); 
    }
}
