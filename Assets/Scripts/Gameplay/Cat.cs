using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidBody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private string _animatorStateParam = "State";
    public Vector2 Position => transform.position;
    public Vector2 Velocity => _rigidBody2D.velocity;

    public void SetVelocity(Vector2 velocity)
    {
        _rigidBody2D.velocity = velocity;
    }

    public void AddForce(Vector2 force, ForceMode2D mode = ForceMode2D.Force)
    {
        _rigidBody2D.AddForce(force, mode);
    }

    public void FlipDirection(float horizontalInput)
    {
        if (horizontalInput != 0)
            _spriteRenderer.flipX = horizontalInput < 0f;
    }
    public void SetAnimationState(EStates state)
    {
        _animator.SetInteger(_animatorStateParam, (int)state);
    }
}
