using System;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class Coin : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var cat = collision.attachedRigidbody.GetComponent<Cat>();
            if(cat != null)
            {
                cat.CollectCoin();
                Destroy(gameObject);
            }
        }
    }
}