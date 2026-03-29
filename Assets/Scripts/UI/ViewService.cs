using Assets.Scripts.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Assets.Scripts.UI
{
    public class ViewService : IInitializable, IDisposable
    {
        private ViewFactory _viewFactory;
        private ViewPrefabConfig _viewPrefabConfig;

        private List<BaseView> _views = new List<BaseView>();

        [Inject]
        public ViewService(
            ViewFactory viewFactory,
            ConfigsService configService)
        {
            _viewFactory = viewFactory;
            _viewPrefabConfig = configService.GetConfig<ViewPrefabConfig>();
        }

        public void Initialize()
        {
        }

        public void Dispose()
        {
            _views.Clear();
        }

        public T GetView<T>() where T : BaseView
        {
            var view = (T)_views.FirstOrDefault(view => view.GetType() == typeof(T));

            if (view == null)
            {
                view = _viewFactory.CreateView(_viewPrefabConfig.GetPrefab<T>());
                _views.Add(view);
            }

            return view;
        }
    }
}