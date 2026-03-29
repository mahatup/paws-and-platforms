using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(DelayConfig),
    menuName = "Configs/Infrastructure/DelayConfig")]
public class DelayConfig : ScriptableObject
{
    [SerializeField] private float _absenceKeyDelay;
    [SerializeField] private float _restartViewDelay;
    [SerializeField] private float _receiveKeyView;

    public float AbsenceKeyDelay => _absenceKeyDelay;
    public float RestartViewDelay => _restartViewDelay;
    public float ReceiveKeyView => _receiveKeyView;
}
