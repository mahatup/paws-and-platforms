using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class MovementBeetleService : MonoBehaviour
{   
    [SerializeField] private Beetle _beetle;
    [SerializeField] private BeetleConfig _config;

    [SerializeField] private LayerMask _catLayer;
    [SerializeField] private Rigidbody2D _catRigidBody2D;

    private float _speed;
    private bool _canTurn = true;
    public void Enable()
    {
        enabled = true;
    }

    public void Disable()
    {
        enabled = false;
    }


    private void Awake()
    {
        _speed = _config.Speed;
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.State != GameState.Playing)
        {
            return;
        }

        Move();
        Knock();
        CheakDeath();
    }

    private void Move()
    {
        Vector2 direction = Vector2.right * Mathf.Sign(_speed);
        Vector2 origin = (Vector2)transform.position + direction * _config.RaycastOriginOffset;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, _config.RaycastForwardDistance);

        if (hit.collider != null)
        {
            TurnAround();
        }

        _beetle.SetVelosity(Vector2.right * _speed);
    }

    private void TurnAround()
    {
        _speed = -_speed;

        _beetle.FlipDirection();
    }

    private void Knock()
    {
        Vector2 direction = Vector2.right * Mathf.Sign(_speed);
        Vector2 origin = (Vector2)transform.position + Vector2.up * _config.RaycastUpOffset + direction * _config.RaycastOriginOffset;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, _config.RaycastForwardDistance, _catLayer);

        if (hit.collider != null)
        {
            Bounce(hit, direction);
            EventManager.OnCatKnocked();
        }
    }

    private void Bounce(RaycastHit2D hit, Vector2 direction)
    {
        _catRigidBody2D.AddForceAtPosition(direction * _config.KnockForce, hit.point, ForceMode2D.Impulse);
        if (_canTurn)
        {
            StartCoroutine(KnockReaction(direction));
        }
    }

    private IEnumerator KnockReaction(Vector2 direction)
    {
        _canTurn = false;

        _beetle.SetVelosity(-direction * Mathf.Abs(_speed));

        yield return new WaitForSeconds(_config.KnockReactionDelay);

        TurnAround();

        yield return new WaitForSeconds(_config.KnockCooldown);
        _canTurn = true;
        Enable();
    }

    private void CheakDeath()
    {
        Vector2 direction = Vector2.up;
        Vector2 origin = (Vector2)transform.position + direction * _config.RaycastOriginOffset;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, _config.DeathRaycastDistance, _catLayer);

        if (hit.collider != null)
        {
            BounceUp(hit);
            StartCoroutine(DieAfterDelay());
        }
    }

    private void BounceUp(RaycastHit2D hit)
    {
        _catRigidBody2D.velocity = new Vector2(_catRigidBody2D.velocity.x, 0f);
        _catRigidBody2D.AddForce(Vector2.up * _config.DeathBounceForce, ForceMode2D.Impulse);
    }

    private IEnumerator DieAfterDelay()
    {
        yield return new WaitForFixedUpdate();
        Dead();
    }

    private void Dead()
    {
        Disable();
        Destroy(gameObject);
    } 
}
