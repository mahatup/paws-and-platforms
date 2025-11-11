using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartViewPresenter : MonoBehaviour
{
    [SerializeField] private ViewPrefabConfig _config;
    [SerializeField] private GameManager _gameManager;

    private StartView _startView;

    private int _currentLine = 0;
    private bool _isActive = true; 

    private ViewFactory viewFactory;

    private void Awake()
    {
        viewFactory = new ViewFactory(_config, transform);
        _startView = viewFactory.CreateStartView();
    }
    private void OnEnable()
    {
        _currentLine = 0;
        _isActive = true;
        
        ShowCurrentLine();
    }

    private void Update()
    {
        if (!_isActive) return;

        if (Input.GetKeyDown(KeyCode.Return))
        {
            ShowNextLine();
        }
    }

    private void ShowCurrentLine()
    {
        if (_currentLine < _startView.LoreLines.Count)
        {
            _startView.SetLoreText(_startView.LoreLines[_currentLine]);
        }
    }

    private void ShowNextLine()
    {
        _currentLine++;
        if (_currentLine < _startView.LoreLines.Count)
        {
            ShowCurrentLine();
        }
        else
        {
            _isActive = false;
            gameObject.SetActive(false);
            _gameManager.StartGame();
        }
    }
}
