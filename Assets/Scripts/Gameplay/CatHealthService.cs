using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CatHealthService
{
    private int _lives;

    public event Action HeartDropped;
    public event Action<int> HeartSpawned;
    public event Action Dead;

    public CatHealthService(int startLives)
    {
        _lives = startLives;
    }
    public void Init()
    {
        HeartSpawned?.Invoke(_lives);
    }

    public void TakeHeart()
    {
        _lives--;
        HeartDropped?.Invoke();

        if (_lives <= 0)
        { 
            Dead?.Invoke(); 
        }
    }
}
