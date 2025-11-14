using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CatMovementService
{
    private Cat _cat;
    private CatConfig _config;
    private float _airTime;

    public event Action AirDeath;

    public CatMovementService(Cat cat, CatConfig config)
    {
        _cat = cat;
        _config = config;
    }

    public void Update(InputService input)
    {
        HandleMovement(input);
        CheckAirDeath();
    }

    public void HandleMovement(InputService input)
    {
        if (IsGrounded())
        { 
            _cat.SetAnimationState(EStates.Idle); 
        }

        if (input.Horizontal != 0)
        { 
            Run(input); 
        }

        if (IsGrounded() && input.JumpPressed)
        { 
            Jump(); 
        }
    }

    public bool IsGroundedPublic()
    {
        return IsGrounded();
    }


    private bool IsGrounded()
    {
        Collider2D[] collider2D = Physics2D.OverlapCircleAll(_cat.transform.position, 0.2f);
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

    private void Run(InputService input)
    {
        if (IsGrounded())
        {
            _cat.SetAnimationState(EStates.Run);
        }

        var horizInput = input.Horizontal;

        _cat.SetVelocity(new Vector2(horizInput * _config.Speed, _cat.GetVelocity().y));
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
