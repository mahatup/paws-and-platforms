using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class Trap : MonoBehaviour, IDamager
    {
        public Vector2 Position2D => new(transform.position.x, transform.position.y);
        public Vector2 Velocity2D => default;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var cat = collision.attachedRigidbody.GetComponent<Cat>();
            if (cat != null)
            {
                cat.SetDamage(this);
            }
        }

    }
}