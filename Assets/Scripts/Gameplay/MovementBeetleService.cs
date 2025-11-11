using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBeetleService : MonoBehaviour
{   
    [SerializeField] private Beetle _beetle;
    [SerializeField] private BeetleConfig _beetleConfig;

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
        _speed = _beetleConfig.Speed;
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
        Vector2 origin = (Vector2)transform.position + direction * 0.6f;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, 0.1f);

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
        Vector2 origin = (Vector2)transform.position + Vector2.up * 0.2f + direction * 0.6f;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, 0.2f, _catLayer);

        if (hit.collider != null)
        {
            _catRigidBody2D.AddForceAtPosition(direction * 8, hit.point, ForceMode2D.Impulse);
            Disable();
            if (_canTurn)
            {
                StartCoroutine(KnockReaction(direction));
                EventManager.OnCatKnocked();
            }
        }
    }
    private IEnumerator KnockReaction(Vector2 direction)
    {
        _canTurn = false;

        _beetle.SetVelosity(-direction * Mathf.Abs(_speed));

        yield return new WaitForSeconds(0.1f);

        TurnAround();

        yield return new WaitForSeconds(_beetleConfig.KnockCooldown);
        _canTurn = true;
        Enable();
    }

    private void CheakDeath()
    {
        Vector2 direction = Vector2.up;
        Vector2 origin = (Vector2)transform.position + direction * 0.5f;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, 0.2f, _catLayer);

        if (hit.collider != null)
        {
            Dead();
        }
    }

    private void Dead()
    {
        Destroy(gameObject);
    } 
}
