using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class CatKnockbackService
    {
        private CatKnockbackConfig _config;

        public CatKnockbackService(CatKnockbackConfig config)
        {
            _config = config;
        }

        public void ApplyKnockback(Cat cat, Vector2 sourcePosition)
        {
            Vector2 delta = cat.Position - sourcePosition;
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
            cat.SetVelocity(Vector2.zero);

            cat.AddForce(knockDirection * _config.KnockForce, ForceMode2D.Impulse);

        }
    }
}