using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartViewPresenter : MonoBehaviour
{
    [SerializeField] private ViewPrefabConfig _viewPrefabConfig;
    [SerializeField] private InputConfig _inputConfig;
    [SerializeField] private GameManager _gameManager;

    private StartView _startView;
    private InputService _inputService;

    private int _currentLine = 0;
    private bool _isActive = true; 

    private ViewFactory viewFactory;

    private void Awake()
    {
        viewFactory = new ViewFactory(transform);
        _startView = viewFactory.CreateView(_viewPrefabConfig.StartViewPrefab);

        _inputService = new InputService(_inputConfig);
    }
    private void OnEnable()
    {
        _currentLine = 0;
        _isActive = true;
        
        ShowLore();
    }

    private void Update()
    {
        if (!_isActive) return;
        if (_inputService.IsSkipPressed)
        {
            ShowNextLoreLine();
        }
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    private void ShowLore()
    {
        if (_currentLine < _startView.LoreLines.Count)
        {
            _startView.SetLoreText(_startView.LoreLines[_currentLine]);
        }
    }

    private void ShowNextLoreLine()
    {
        _currentLine++;
        if (_currentLine < _startView.LoreLines.Count)
        {
            ShowLore();
        }
        else
        {
            _isActive = false;
            gameObject.SetActive(false);
            _gameManager.StartGame();
        }
    }
}
