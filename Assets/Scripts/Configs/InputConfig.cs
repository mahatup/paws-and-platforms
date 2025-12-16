using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(InputConfig),
    menuName = "Configs/Core/InputConfig ")]
public class InputConfig : ScriptableObject
{
    [SerializeField] private string _axis = "Horizontal";
    [SerializeField] private KeyCode _jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode _skipKey = KeyCode.Return;

    public string Axis => _axis;
    public KeyCode JumpKey => _jumpKey;
    public KeyCode SkipKey => _skipKey;
}
