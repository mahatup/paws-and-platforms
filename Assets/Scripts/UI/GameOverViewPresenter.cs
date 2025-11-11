using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverViewPresenter : MonoBehaviour
{
    [SerializeField] private ViewPrefabConfig _config;
    [SerializeField] private LevelExitService _levelExitService;

    private ViewFactory _viewFactory;
    private GameOverView _gameOverView;

    private void Awake()
    {
        _viewFactory = new ViewFactory(_config, transform);
        _viewFactory.CreateGameOverView();

        _levelExitService.LevelCompleted += OnLevelCompleted;
    }


    private void OnDisable()
    {
        _levelExitService.LevelCompleted -= OnLevelCompleted;
    }

    private void OnLevelCompleted()
    {
    }
}
