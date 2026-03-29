using System;

namespace Assets.Scripts.Gameplay
{
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

        public void DecreaseHealth()
        {
            _lives--;
            HeartDropped?.Invoke();

            if (_lives <= 0)
            {
                Dead?.Invoke();
            }
        }
    }
}