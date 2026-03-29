using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class Beetle : MonoBehaviour, IDamager
    {
        [SerializeField] private Rigidbody2D _rigidBody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public Vector2 Position2D => new(transform.position.x, transform.position.y);

        public void SetVelosity(Vector2 velocity)
        {
            _rigidBody2D.velocity = velocity;
        }

        public void FlipDirection()
        {
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
    }
}