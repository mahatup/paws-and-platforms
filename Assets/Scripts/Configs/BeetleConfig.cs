using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(BeetleConfig),
    menuName = "Configs/Core/BeetleConfig")]
public class BeetleConfig : ScriptableObject
{
    [SerializeField] private float _speed;

    [SerializeField, Range(0.1f, 1f)] private float _knockCooldown = 0.5f;

    public float Speed => _speed;
    public float KnockCooldown => _knockCooldown;
}
