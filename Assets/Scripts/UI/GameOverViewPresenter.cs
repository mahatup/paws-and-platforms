using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//TODO: обдумать данную сущность
public class GameOverViewPresenter : MonoBehaviour    
{
    [SerializeField] private ViewPrefabConfig _config;
    [SerializeField] private GameManager _gameManager;

    private ViewFactory _viewFactory;
    private GameOverView _gameOverView;

    private void Awake()
    {
        _viewFactory = new ViewFactory(transform);
        _gameOverView = _viewFactory.CreateView(_config.GameOverViewPrefab);

        _gameManager.LevelCompleted += OnLevelCompleted;
    }

    private void OnDisable()
    {
        _gameManager.LevelCompleted -= OnLevelCompleted;
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    private void OnLevelCompleted()
    {
    }
}
