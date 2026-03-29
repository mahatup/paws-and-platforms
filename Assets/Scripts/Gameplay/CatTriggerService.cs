using System;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class CatTriggerService
    {
        public event Action CoinCollected;
        public event Action KeyCollected;
        public event Action SpaceShipStepped;

        public void SetCollision(Collider2D collision)
        {
            if (collision.TryGetComponent(out Coin _))
            {
                CoinCollected?.Invoke();
                UnityEngine.Object.Destroy(collision.gameObject);
            }

            else if (collision.TryGetComponent(out Key _))
            {
                KeyCollected?.Invoke();
                UnityEngine.Object.Destroy(collision.gameObject);
            }

            else if (collision.TryGetComponent(out SpaceShip _))
            {
                SpaceShipStepped?.Invoke();
            }
        }
    }
}