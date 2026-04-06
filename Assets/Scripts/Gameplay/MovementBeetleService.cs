using Assets.Scripts.Configs;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UIElements;
using Zenject;

namespace Assets.Scripts.Gameplay
{
    public class MovementBeetleService : MonoBehaviour
    {
        private Beetle _beetle;
        private BeetleConfig _config;
        private float _speed;
        
        public void Initialize(Beetle beetle, BeetleConfig config)
        {
            _beetle = beetle;
            _config = config;
        }

        private void Awake()
        {
            _speed = _config.Speed;
        }

        void FixedUpdate()
        {
            Vector2 direction = Vector2.right * Mathf.Sign(_speed);
            Vector2 origin = _beetle.Position2D + direction * _config.RaycastOriginOffset;
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, _config.RaycastForwardDistance);

            if (hit.collider != null)
            {
                _speed = -_speed;
                _beetle.FlipDirection();
            }

            _beetle.SetVelocity(Vector2.right * _speed);
        }
    }
}