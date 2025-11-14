using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private LayerMask _catLayer;
    [SerializeField] private float _cooldownTime;
    [SerializeField] private float _radius;

    private bool _isOnCooldown = false;

    private void FixedUpdate()
    {
        Collider2D cat = Physics2D.OverlapCircle(transform.position, _radius, _catLayer);
        if (!_isOnCooldown && cat != null)
        {
            EventManager.OnCatKnocked(transform.position, Vector2.zero);
            StartCoroutine(Cooldown());
        }
    }

    private IEnumerator Cooldown()
    {
        _isOnCooldown = true;
        yield return new WaitForSeconds(_cooldownTime);
        _isOnCooldown = false;
    }
}