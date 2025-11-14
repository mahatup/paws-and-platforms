using System;
using UnityEngine;
public class EventManager
{
    public static event Action<Vector2, Vector2> CatKnocked;
    public static event Action<Vector2, Vector2> EnemyKilled;

    public static void OnCatKnocked(Vector2 sourcePosition, Vector2 sourceVelocity)
    {
        CatKnocked?.Invoke(sourcePosition, sourceVelocity);
    }

    public static void OnEnemyKilled(Vector2 sourcePosition, Vector2 sourceVelocity)
    {
        EnemyKilled?.Invoke(sourcePosition, sourceVelocity);
    }
}
