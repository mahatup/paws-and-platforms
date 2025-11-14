using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = nameof(BeetleConfig),
    menuName = "Configs/Core/BeetleConfig")]
public class BeetleConfig : ScriptableObject
{
    [SerializeField] private float _speed;

    [SerializeField] private float _raycastForwardDistance = 0.1f;
    [SerializeField] private float _raycastOriginOffset = 0.6f;
    [SerializeField] private float _raycastUpOffset = 0.2f;
    [SerializeField] private float _deathRaycastDistance = 1f;


    public float Speed => _speed;

    public float RaycastForwardDistance => _raycastForwardDistance;
    public float RaycastOriginOffset => _raycastOriginOffset;
    public float RaycastUpOffset => _raycastUpOffset;
    public float DeathRaycastDistance => _deathRaycastDistance;

}
