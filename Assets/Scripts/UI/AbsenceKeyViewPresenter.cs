using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading;

public class AbsenceKeyViewPresenter : MonoBehaviour
{
    [SerializeField] private ViewPrefabConfig _viewConfig;
    [SerializeField] private LevelExitService _levelExitService;

    private ViewFactory _viewFactory;

    private void Awake()
    {
        _viewFactory = new ViewFactory(_viewConfig, transform);
    }

    private void OnEnable()
    {
        _levelExitService.KeyNotCollected += OnKeyNotCollected;
    }

    private void OnDisable()
    {
        _levelExitService.KeyNotCollected -= OnKeyNotCollected;
    }

    private void OnKeyNotCollected()
    {
        ShowTemporaryMessageAsync(this.GetCancellationTokenOnDestroy()).Forget();
    }

    private async UniTaskVoid ShowTemporaryMessageAsync(CancellationToken token)
    {
        var view = _viewFactory.CreateAbsenceKeyView();

        await UniTask.Delay(TimeSpan.FromSeconds(2), cancellationToken: token);

        Destroy(view.gameObject);
    }
}
