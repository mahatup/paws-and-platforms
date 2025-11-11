using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(ViewPrefabConfig),
    menuName = "Configs/UI/ViewPrefabConfig")]
public class ViewPrefabConfig : ScriptableObject
{
    [SerializeField] private CoinCounterView _coinCounterViewPrefab;
    [SerializeField] private ReceiveKeyView _receiveKeyViewPrefab;
    [SerializeField] private LivesView _livesViewPrefab;
    [SerializeField] private RestartView _restartViewPrefab;
    [SerializeField] private AbsenceKeyView _absenceKeyViewPrefab;
    [SerializeField] private StartView _startViewPrefab;
    [SerializeField] private GameOverView _gameOverViewPrefab;
    
    public CoinCounterView CoinCounterViewPrefab => _coinCounterViewPrefab;
    public ReceiveKeyView ReceiveKeyViewPrefab => _receiveKeyViewPrefab;
    public LivesView LivesViewPrefab => _livesViewPrefab;
    public RestartView RestartViewPrefab => _restartViewPrefab;
    public AbsenceKeyView AbsenceKeyViewPrefab => _absenceKeyViewPrefab;
    public StartView StartViewPrefab => _startViewPrefab;
    public GameOverView GameOverViewPrefab => _gameOverViewPrefab;
}
