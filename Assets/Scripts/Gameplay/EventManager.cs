using System;
public class EventManager
{
    public static event Action CatKnocked;

    public static void OnCatKnocked()
    {
        CatKnocked?.Invoke();
    }
}
