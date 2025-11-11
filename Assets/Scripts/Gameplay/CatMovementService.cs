using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CatMovementService
{
    private Cat _cat;
    private CatConfig _config;

    public CatMovementService(Cat cat, CatConfig config)
    {
        _cat = cat;
        _config = config;
    }

    public void HandleInput()
    {
        if (IsGrounded())
            _cat.SetAnimationState(EStates.Idle);

        if (Input.GetButton("Horizontal"))
            Run();

        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    public bool IsGroundedPublic()
    {
        return IsGrounded();
    }

    private void Run()
    {
        if (IsGrounded())
        {
            _cat.SetAnimationState(EStates.Run);
        }

        var horizInput = Input.GetAxis("Horizontal");

        _cat.SetVelocity(new Vector2(horizInput * _config.Speed, _cat.GetVelocity().y));
        _cat.FlipDirection(horizInput);
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

    private void Jump()
    {
        _cat.AddForce(Vector2.up * _config.JumpForce, ForceMode2D.Impulse);

    }

}
