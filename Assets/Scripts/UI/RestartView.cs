using System;
using UnityEngine;
using UnityEngine.UI;

public class RestartView : MonoBehaviour
{
    [SerializeField] private Button _restartGameButton;

    public event Action RestartGameButtonClicked;

    private void OnEnable()
    {
        _restartGameButton.onClick.AddListener(OnRestartGameButton);
    }
    private void OnDisable()
    {
        _restartGameButton.onClick.RemoveListener(OnRestartGameButton);
    }
    private void OnRestartGameButton()
    {
        RestartGameButtonClicked?.Invoke();
    }
}
