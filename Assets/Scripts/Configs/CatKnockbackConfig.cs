using System;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(CatKnockbackConfig),
    menuName = "Configs/Core/CatKnockbackConfig")]
public class CatKnockbackConfig : ScriptableObject
{
    [SerializeField] private uint _knockForce;
    [SerializeField] private float _knockbackSideHorizontal;
    [SerializeField] private float _knockbackSideVertical;

    public float KnockForce => _knockForce;
    public float KnockbackSideHorizontal => _knockbackSideHorizontal;
    public float KnockbackSideVertical => _knockbackSideVertical;
}
