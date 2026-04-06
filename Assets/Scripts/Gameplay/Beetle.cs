using Assets.Scripts.Configs;
using System;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Assets.Scripts.Gameplay
{
    public class Beetle : MonoBehaviour, IDamager
    {
        [SerializeField] private Rigidbody2D _rigidBody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private MovementBeetleService _movementService;

        private BeetleConfig _beetleConfig;

        [Inject]
        public void Construct(ConfigsService configService)
        {
            _beetleConfig = configService.GetConfig<BeetleConfig>();
        }

        private void Awake()
        {
            _movementService.Initialize(this, _beetleConfig);
        }

        public Vector2 Position2D => new(transform.position.x, transform.position.y);

        public void SetVelocity(Vector2 velocity)
        {
            _rigidBody2D.velocity = velocity;
        }

        public void FlipDirection()
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out Cat cat))
            {
                ContactPoint2D contact = collision.contacts[0];
                if (contact.normal.y < 0)
                {
                    cat.Push(this);
                    Destroy(gameObject);
                }
                else 
                {
                    cat.SetDamage(this);
                }
            }
        }
    }
}
    