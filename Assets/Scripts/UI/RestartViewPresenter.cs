using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartViewPresenter : MonoBehaviour
{
    [SerializeField] private ViewPrefabConfig _config;
    [SerializeField] private float _delay;
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private CatService _catService;

    private ViewFactory _viewFactory;
    private RestartView _restartView;

    private CancellationTokenSource _restartSceneCts;

    private void Awake()
    {
        _viewFactory = new ViewFactory(_config, transform);
        _restartView = _viewFactory.CreateRestartView();
    }

    private void OnEnable()
    {
        _restartView.RestartGameButtonClicked += OnRestartGameButtonClicked;
        _catService.Dead += Restart;
    }

    private void OnDisable()
    {
        _restartView.RestartGameButtonClicked -= OnRestartGameButtonClicked;
        _catService.Dead -= Restart;
    }

    private void OnDestroy()
    {
        if (_restartSceneCts != null)
        {
            _restartSceneCts.Cancel();
            _restartSceneCts.Dispose();
            _restartSceneCts = null;
        }
    }

    private void OnRestartGameButtonClicked()
    {
        Restart();
    }

    public void Restart()
    {
        _restartSceneCts = new CancellationTokenSource();
        RestartAfterDelayAsync(_restartSceneCts.Token).Forget();
    }

    public async UniTaskVoid RestartAfterDelayAsync(CancellationToken token)
    {
        await UniTask.Delay(TimeSpan.FromSeconds(_delay), cancellationToken: token);

        GameManager.StaticSkipIntroNextLoad = true;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }
}
