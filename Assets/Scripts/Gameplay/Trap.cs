using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private LayerMask _catLayer;
    [SerializeField] private float _cooldownTime;

    private Collider2D _trapCollider;
    private bool _isOnCooldown = false;

    private void Awake()
    {
        _trapCollider = GetComponent<Collider2D>();
    }
    public void Enable()
    {
        enabled = true;
        if (_trapCollider != null)
            _trapCollider.enabled = true;
    }

    public void Disable()
    {
        enabled = false;
        if (_trapCollider != null)
            _trapCollider.enabled = false;
    }

    private void FixedUpdate()
    {
        if(!_isOnCooldown && TryGetCatHit(out Collider2D hit, out Rigidbody2D catRigidBody2D))
        {
            Knock(hit, catRigidBody2D);
        }
    }

    private bool TryGetCatHit(out Collider2D hit, out Rigidbody2D catRigidBody2D)
    {
        hit = Physics2D.OverlapCircle(transform.position, 1f, _catLayer);

        if (hit != null)
        {
            catRigidBody2D = hit.attachedRigidbody;
            return true;
        }

        catRigidBody2D = null;
        return false;
    }

    private void Knock(Collider2D hit, Rigidbody2D catRigidBody2D)
    {
        if (catRigidBody2D != null)
        {
            EventManager.OnCatKnocked();
            StartCoroutine(Cooldown());
        }
    }
    private IEnumerator Cooldown()
    {
        _isOnCooldown = true;
        Disable();
        yield return new WaitForSeconds(_cooldownTime);
        _isOnCooldown = false;
        Enable();
    }
}