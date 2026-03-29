using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class LivesView : BaseView
    {
        [SerializeField] private GameObject _heartTemplate;
        [SerializeField] private Transform _container;

        private readonly List<Heart> _hearts = new();

        public void Initialize(int lives)
        {
            Clear();

            for (int i = 0; i < lives; i++)
            {
                CreateHeart();
            }
        }

        public void BreakLastHeart()
        {
            if (_hearts.Count == 0) return;

            Heart last = _hearts[^1];

            last.Whole.SetActive(false);
            last.Broken.SetActive(true);

            _hearts.RemoveAt(_hearts.Count - 1);
        }

        private void CreateHeart()
        {
            var heartObj = Instantiate(_heartTemplate, _container);

            var whole = heartObj.transform.Find("WholeHeart").gameObject;
            var broken = heartObj.transform.Find("BrokenHeart").gameObject;

            broken.SetActive(false);

            _hearts.Add(new Heart
            {
                Whole = whole,
                Broken = broken
            });
        }

        private void Clear()
        {
            foreach (Transform child in _container)
                Destroy(child.gameObject);

            _hearts.Clear();
        }
    }
}