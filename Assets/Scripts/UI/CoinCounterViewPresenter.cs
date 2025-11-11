using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCounterViewPresenter : MonoBehaviour
{
    [SerializeField] private CatService _catService;
    [SerializeField] private ViewPrefabConfig _config;

    private CoinCounterView _coinCounterView;

    private int _coinCounter;
    private ViewFactory _viewFactory;

    private void Awake()
    {
        _viewFactory = new ViewFactory(_config, transform);
        _coinCounterView = _viewFactory.CreateCoinCounterView();
    }

    private void OnEnable()
    {
        _catService.CoinCollected += OnCoinCollected;
    }

    private void OnDisable()
    {
        _catService.CoinCollected -= OnCoinCollected;
    }

    private void OnCoinCollected()
    {
        _coinCounter++;
        _coinCounterView.SetCounterCoin(_coinCounter);
    }
}
