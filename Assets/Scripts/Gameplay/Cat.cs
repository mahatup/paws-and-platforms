using System;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class Cat : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidBody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Animator _animator;
        [SerializeField] private string _animatorStateParam = "State";
        public Vector2 Position => transform.position;
        public Vector2 Velocity => _rigidBody2D.velocity;

        public event Action CoinCollected;
        public event Action KeyCollected;
        public event Action SpaceShipStepped;
        public event Action LossLife;
        public event Action<IDamager> Pushing;
        public event Action<IDamager> EnemyKilled;

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

        public void CollectCoin()
        {
            CoinCollected?.Invoke();
        }
        public void CollectKey()
        {
            KeyCollected?.Invoke();
        }
        public void CollectSpaceShip()
        {
            SpaceShipStepped?.Invoke();
        }
        public void SetDamage(IDamager damager)
        {
            Pushing?.Invoke(damager);
            LossLife?.Invoke();
        }
        public void Push(IDamager damager)
        {
            Pushing?.Invoke(damager);
        }
        public void BeetleDeath(IDamager damager)
        {
            EnemyKilled?.Invoke(damager);
        }
    }
}