using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class AbsenceKeyViewPresenter : MonoBehaviour
{
    [SerializeField] private ViewPrefabConfig _config;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private float _messageDuration;
    private ViewFactory _viewFactory;

    private void Awake()
    {
        _viewFactory = new ViewFactory(transform);
    }

    private void OnEnable()
    {
        _gameManager.KeyNotCollected += OnKeyNotCollected;
    }

    private void OnDisable()
    {
        _gameManager.KeyNotCollected -= OnKeyNotCollected;
    }

    private void OnKeyNotCollected()
    {
        ShowTemporaryMessageAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }

    private async UniTaskVoid ShowTemporaryMessageAsync(CancellationToken token)
    {
        var view = _viewFactory.CreateView(_config.AbsenceKeyViewPrefab);

        await UniTask.Delay(TimeSpan.FromSeconds(_messageDuration), cancellationToken: token);

        Destroy(view.gameObject);
    }
}
