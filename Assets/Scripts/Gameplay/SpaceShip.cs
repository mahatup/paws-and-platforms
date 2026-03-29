using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class SpaceShip : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var cat = collision.attachedRigidbody.GetComponent<Cat>();
            if (cat != null)
            {
                cat.CollectSpaceShip();
            }
        }
    }
}