using UnityEngine;
using Zenject;

namespace Assets.Scripts.UI
{
    public class ViewFactory : IInitializable
    {
        private GameObject _prefabViewContainer;
        private GameObject _viewsContainer;

        [Inject]
        public ViewFactory(GameObject prefabView)
        {
            _prefabViewContainer = prefabView;
            _viewsContainer = Object.Instantiate(_prefabViewContainer);
        }

        public void Initialize()
        {

        }

        public T CreateView<T>(T prefab) where T : BaseView
        {
            return Object.Instantiate(prefab, _viewsContainer.transform);
        }
    }
}