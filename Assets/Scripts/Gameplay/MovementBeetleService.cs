using Assets.Scripts.Configs;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay
{
    public class MovementBeetleService : MonoBehaviour
    {
        [SerializeField] private Beetle _beetle;
        [SerializeField] private LayerMask _catLayer;

        private GameManager _gameManager;
        private BeetleConfig _beetleConfig;

        private float _speed;

        private CancellationTokenSource _deathCts;

        [Inject]
        public void Construct(
            GameManager gameManager,
            ConfigsService configService)
        {
            _gameManager = gameManager;
            _beetleConfig = configService.GetConfig<BeetleConfig>();
        }

        private void Awake()
        {
            _speed = _beetleConfig.Speed;

            _deathCts = new CancellationTokenSource();
        }

        void FixedUpdate()
        {
            if (_gameManager.State != GameState.Playing)
            {
                return;
            }

            Move();
            Knock();
            CheakDeath();
        }

        private void OnDestroy()
        {
            if (_deathCts != null)
            {
                _deathCts.Cancel();
                _deathCts.Dispose();
                _deathCts = null;
            }
        }

        private void Move()
        {
            Vector2 direction = Vector2.right * Mathf.Sign(_speed);
            Vector2 origin = (Vector2)transform.position + direction * _beetleConfig.RaycastOriginOffset;
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, _beetleConfig.RaycastForwardDistance);

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
            Vector2 origin = _beetle.Position2D + Vector2.up * _beetleConfig.RaycastUpOffset + direction * _beetleConfig.RaycastOriginOffset;
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, _beetleConfig.RaycastForwardDistance, _catLayer);

            if (hit.collider != null)
            {
                EventManager.OnCatKnocked(_beetle);
            }
        }

        private void CheakDeath()
        {
            Vector2 direction = Vector2.up;
            Vector2 origin = (Vector2)transform.position + direction * _beetleConfig.RaycastOriginOffset;
            RaycastHit2D hit = Physics2D.Raycast(origin, direction, _beetleConfig.DeathRaycastDistance, _catLayer);

            if (hit.collider != null)
            {
                EventManager.OnEnemyKilled(_beetle);
                DieAfterDelayAsync(_deathCts.Token).Forget();
            }
        }

        private async UniTaskVoid DieAfterDelayAsync(CancellationToken token)
        {
            await UniTask.WaitForFixedUpdate();

            Dead();
        }

        private void Dead()
        {
            Destroy(gameObject);
        }
    }
}