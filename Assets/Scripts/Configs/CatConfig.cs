using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = nameof(CatConfig),
    menuName = "Configs/Core/CatConfig")]

public class CatConfig : ScriptableObject
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _knockForce;
    [SerializeField] private int _lives;
    [SerializeField] private float _maxAirTime;
    public float Speed => _speed;
    public float JumpForce => _jumpForce;
    public float KnockForce => _knockForce;
    public int Lives => _lives;
    public float MaxAirTime => _maxAirTime;

}
