using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewFactory
{
    private ViewPrefabConfig _config;
    private Transform _parent;

    public ViewFactory(ViewPrefabConfig config, Transform parent)
    {
        _config = config;
        _parent = parent;
    }

    public LivesView CreateLivesView()
    {
        return Object.Instantiate(_config.LivesViewPrefab, _parent);
    }

    public CoinCounterView CreateCoinCounterView()
    {
        return Object.Instantiate(_config.CoinCounterViewPrefab, _parent);
    }
    public ReceiveKeyView CreateReceiveKeyView()
    {
        return Object.Instantiate(_config.ReceiveKeyViewPrefab, _parent);
    }
    public RestartView CreateRestartView()
    {
        return Object.Instantiate(_config.RestartViewPrefab, _parent);
    }
    public AbsenceKeyView CreateAbsenceKeyView()
    {
        return Object.Instantiate(_config.AbsenceKeyViewPrefab, _parent);
    }
    public StartView CreateStartView()
    {
        return Object.Instantiate(_config.StartViewPrefab, _parent);
    }
    public GameOverView CreateGameOverView()
    {
        return Object.Instantiate(_config.GameOverViewPrefab, _parent);
    }
}
