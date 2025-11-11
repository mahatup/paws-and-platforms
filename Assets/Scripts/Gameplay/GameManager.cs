using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Intro,
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private StartViewPresenter _startViewPresenter;
    [SerializeField] private GameOverViewPresenter _gameOverViewPresenter;

    public static bool StaticSkipIntroNextLoad = false;
    public static GameManager Instance { get; private set; }
    
    public GameState State { get; private set; } = GameState.Intro;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
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

    public void EnterIntroState()
    {
        State = GameState.Intro;

        _startViewPresenter.gameObject.SetActive(true);

        if (_gameOverViewPresenter != null)
            _gameOverViewPresenter.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        State = GameState.Playing;

        _startViewPresenter.gameObject.SetActive(false);
    }

    public void EnterGameOverState()
    {
        State = GameState.GameOver;

        if (_gameOverViewPresenter != null)
        {
            _gameOverViewPresenter.gameObject.SetActive(true);
        }
            
    }
}
