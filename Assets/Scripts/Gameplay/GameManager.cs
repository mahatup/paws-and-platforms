using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public enum GameState
{
    Intro,
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private CatService _catService;
    [SerializeField] private ReceiveKeyViewPresenter _receiveKeyViewPresenter;
    [SerializeField] private StartViewPresenter _startViewPresenter;
    [SerializeField] private GameOverViewPresenter _gameOverViewPresenter;

    private bool _isKeyCollected = false;

    public static bool StaticSkipIntroNextLoad = false;
    public static GameManager Instance { get; private set; }
    
    public GameState State { get; private set; } = GameState.Intro;

    public event Action LevelCompleted;
    public event Action KeyNotCollected;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void OnEnable()
    {
        _catService.SpaceShipStepped += OnSpaceShipStepped;
        _receiveKeyViewPresenter.KeyCollected += OnKeyCollected;
    }

    private void Start()
    {
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
    private void OnDisable()
    {
        _catService.SpaceShipStepped -= OnSpaceShipStepped;
        _receiveKeyViewPresenter.KeyCollected -= OnKeyCollected;
    }

    private void OnSpaceShipStepped()
    {
        if (_isKeyCollected)
        {
            EnterGameOverState();
            LevelCompleted?.Invoke();

            if (_receiveKeyViewPresenter != null)
                _receiveKeyViewPresenter.KeyCollected -= OnKeyCollected;
        }
        else
        {
            KeyNotCollected?.Invoke();
        }
    }

    private void OnKeyCollected()
    {
        _isKeyCollected = true;
    }
    public void EnterIntroState()
    {
        State = GameState.Intro;
        _startViewPresenter.SetActive(true);

        if (_gameOverViewPresenter != null)
            _gameOverViewPresenter.SetActive(false);
    }

    public void StartGame()
    {
        State = GameState.Playing;
        _startViewPresenter.SetActive(false);
    }

    public void EnterGameOverState()
    {
        State = GameState.GameOver;

        if (_gameOverViewPresenter != null)
        {
            _gameOverViewPresenter.SetActive(true);
        }
    }
}
