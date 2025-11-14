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

    private float _speed;

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
        Vector2 origin = _beetle.Position + Vector2.up * _config.RaycastUpOffset + direction * _config.RaycastOriginOffset;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, _config.RaycastForwardDistance, _catLayer);

        if (hit.collider != null)
        {
            EventManager.OnCatKnocked(_beetle.Position, _beetle.Velocity);
        }
    }

    private void CheakDeath()
    {
        Vector2 direction = Vector2.up;
        Vector2 origin = (Vector2)transform.position + direction * _config.RaycastOriginOffset;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, _config.DeathRaycastDistance, _catLayer);

        if (hit.collider != null)
        {
            EventManager.OnEnemyKilled(_beetle.Position, Vector2.zero);
            StartCoroutine(DieAfterDelay());
        }
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
