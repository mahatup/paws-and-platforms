using System;
using UnityEngine;

namespace Assets.Scripts.Gameplay
{
    public class EventManager
    {
        public static event Action<IDamager> CatKnocked;
        public static event Action<IDamager> EnemyKilled;

        public static void OnCatKnocked(IDamager damager)
        {
            CatKnocked?.Invoke(damager);
        }

        public static void OnEnemyKilled(IDamager damager)
        {
            EnemyKilled?.Invoke(damager);
        }
    }
}