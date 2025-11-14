using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class CatKnockbackService
{
    private Cat _cat;
    private CatConfig _config;

    public CatKnockbackService(Cat cat, CatConfig catConfig)
    {
        _cat = cat;
        _config = catConfig;
    }

    public void ApplyKnockback(Vector2 sourcePosition, Vector2 sourceVelocity)
    {
        Vector2 delta = _cat.Position - sourcePosition;
        Vector2 knockDirection;

        bool hitFromAbove = Mathf.Abs(delta.y) > Mathf.Abs(delta.x) && delta.y < 0;
        bool hitFromBelow = Mathf.Abs(delta.y) > Mathf.Abs(delta.x) && delta.y > 0;

        if (hitFromAbove)
        { 
            knockDirection = Vector2.down;
        } 
        else if (hitFromBelow)
        { 
            knockDirection = Vector2.up;
        }
        else
        {
            float horizontalSign = delta.x != 0 ? Mathf.Sign(delta.x) : 1f;
            knockDirection = new Vector2(horizontalSign * _config.KnockbackSideHorizontal, _config.KnockbackSideVertical).normalized;

        }
        _cat.SetVelocity(Vector2.zero);

        _cat.AddForce(knockDirection * _config.KnockForce, ForceMode2D.Impulse);

    }
}
