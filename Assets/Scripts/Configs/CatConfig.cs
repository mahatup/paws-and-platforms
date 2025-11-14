using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = nameof(CatConfig),
    menuName = "Configs/Core/CatConfig")]

public class CatConfig : ScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _lives;
    [SerializeField] private float _maxAirTime;

    [Header("Knockback")]
    [SerializeField] private float _knockForce;
    [SerializeField] private float _knockbackSideHorizontal;
    [SerializeField] private float _knockbackSideVertical;

    public float Speed => _speed;
    public float JumpForce => _jumpForce;
    public int Lives => _lives;
    public float MaxAirTime => _maxAirTime;
    public float KnockForce => _knockForce;
    public float KnockbackSideHorizontal => _knockbackSideHorizontal;
    public float KnockbackSideVertical => _knockbackSideVertical;
}
