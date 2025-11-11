using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelExitService : MonoBehaviour
{
    [SerializeField] private CatService _catService;
    [SerializeField] private ReceiveKeyViewPresenter _receiveKeyViewPresenter;
    [SerializeField] private GameManager _gameManager;

    private bool _isKeyCollected = false;

    public event Action KeyNotCollected;
    public event Action LevelCompleted;

    private void OnEnable()
    {
        _catService.SpaceShipStepped += OnSpaceShipStepped;
        _receiveKeyViewPresenter.KeyCollected += OnKeyCollected;
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
            _gameManager.EnterGameOverState();
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
}
