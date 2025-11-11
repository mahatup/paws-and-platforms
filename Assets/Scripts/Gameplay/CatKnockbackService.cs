using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CatKnockbackService
{
    private Cat _cat;

    public CatKnockbackService(Cat cat)
    {
        _cat = cat;
    }

    public void ApplyKnockback(Vector3 catPosition)
    {
        Collider2D[] traps = Physics2D.OverlapCircleAll(catPosition, 2f);
        foreach (var trap in traps)
        {
            Vector2 knockDirection = (catPosition - trap.transform.position).normalized;
            knockDirection += Vector2.up * 0.5f;
            knockDirection = knockDirection.normalized;

            _cat.SetVelocity(Vector2.zero);
            _cat.AddForce(knockDirection * 10f, ForceMode2D.Impulse);
            break;
        }
    }
}
