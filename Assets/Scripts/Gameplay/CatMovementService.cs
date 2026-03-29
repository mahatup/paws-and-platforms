using Assets.Scripts.Configs;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay
{
    public class CatMovementService
    {
        private Cat _cat;
        private CatConfig _catConfig;
        private float _airTime;
        private InputService _inputService;

        public event Action AirDeath;

        [Inject]
        public CatMovementService(Cat cat, ConfigsService configService, InputService inputService)
        {
            _cat = cat;
            _catConfig = configService.GetConfig<CatConfig>();
            _inputService = inputService;
        }

        public void Update()
        {
            HandleMovement();
            CheckAirDeath();
        }

        private void HandleMovement()
        {
            if (IsGrounded())
            {
                _cat.SetAnimationState(EStates.Idle);
            }

            if (_inputService.MoveAxis != 0)
            {
                Run();
            }

            if (IsGrounded() && _inputService.IsJumpPressed)
            {
                Jump();
            }
        }

        private bool IsGrounded()
        {
            var collider2D = Physics2D.OverlapCircleAll(_cat.Position, _catConfig.GroundRadius);
            if (collider2D.Length > 1)
            {
                return true;
            }
            else
            {
                _cat.SetAnimationState(EStates.Jump);
                return false;
            }
        }

        private void Run()
        {
            if (IsGrounded())
            {
                _cat.SetAnimationState(EStates.Run);
            }

            var horizInput = _inputService.MoveAxis;

            _cat.SetVelocity(new Vector2(horizInput * _catConfig.Speed, _cat.Velocity.y));
            _cat.FlipDirection(horizInput);
        }

        private void Jump()
        {
            _cat.AddForce(Vector2.up * _catConfig.JumpForce, ForceMode2D.Impulse);
        }

        private void CheckAirDeath()
        {
            if (!IsGrounded())
            {
                _airTime += Time.deltaTime;
                if (_airTime >= _catConfig.MaxAirTime)
                {
                    AirDeath?.Invoke();
                    _airTime = 0;
                }
            }
            else
            {
                _airTime = 0;
            }
        }
    }
}