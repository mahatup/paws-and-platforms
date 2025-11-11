using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatTriggerService
{
    public event Action CoinCollected;
    public event Action KeyCollected;
    public event Action SpaceShipStepped;

    public void HandleTrigger(Collider2D collision)
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
