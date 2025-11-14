using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beetle : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public Vector2 Position => transform.position;
    public Vector2 Velocity => _rigidBody2D.velocity;

    public void SetVelosity(Vector2 velocity)
    {
        _rigidBody2D.velocity = velocity;
    }
    
    public void FlipDirection()
    {
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }
}
