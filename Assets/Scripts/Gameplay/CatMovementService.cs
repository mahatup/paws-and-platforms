using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatMovementService
{
    private Cat _cat;
    private CatConfig _config;
    private float _airTime;
    private InputService _inputService;

    public event Action AirDeath;

    public CatMovementService(Cat cat, CatConfig config, InputService inputService)
    {
        _cat = cat;
        _config = config;
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
        var collider2D = Physics2D.OverlapCircleAll(_cat.Position, 0.2f);
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

        _cat.SetVelocity(new Vector2(horizInput * _config.Speed, _cat.Velocity.y));
        _cat.FlipDirection(horizInput);
    }

    private void Jump()
    {
        _cat.AddForce(Vector2.up * _config.JumpForce, ForceMode2D.Impulse);
    }

    private void CheckAirDeath()
    {
        if (!IsGrounded())
        {
            _airTime += Time.deltaTime;
            if (_airTime >= _config.MaxAirTime)
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
