using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveKeyViewPresenter : MonoBehaviour
{
    [SerializeField] private CatService _catService;
    [SerializeField] private ViewPrefabConfig _viewConfig;

    private ViewFactory _viewFactory;
    private ReceiveKeyView _receiveKeyView;

    public event Action KeyCollected;

    private void Awake()
    {
        _viewFactory = new ViewFactory(_viewConfig, transform);
    }

    private void OnEnable()
    {
        _catService.KeyCollected += OnKeyCollected;
    }

    private void OnDisable()
    {
        _catService.KeyCollected -= OnKeyCollected;
    }

    private void OnKeyCollected()
    {
        _receiveKeyView = _viewFactory.CreateReceiveKeyView();
        KeyCollected?.Invoke();
        ClearNotificationAfterDelay(2f); 
    }

    private async void ClearNotificationAfterDelay(float delaySeconds)
    {
        await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(delaySeconds));

        if (_receiveKeyView != null)
            _receiveKeyView.ClearText();
    }
}

