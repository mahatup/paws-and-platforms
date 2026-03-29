using Assets.Scripts.Configs;
using Assets.Scripts.Gameplay;
using Assets.Scripts.UI;
using UnityEngine;
using Zenject;

public class GameplaySceneInstaller : MonoInstaller
{
    [SerializeField] private GameObject _prefabView;

    [SerializeField] private ViewPrefabConfig _viewPrefabConfig;
    [SerializeField] private BeetleConfig _beetleConfig;
    [SerializeField] private CatConfig _catConfig;
    [SerializeField] private CatKnockbackConfig _catKnockbackConfig;
    [SerializeField] private InputConfig _inputConfig;
    [SerializeField] private ConfigsServiceConfig _configsServiceConfig;

    public override void InstallBindings()
    {
        BindConfig();
        BindInput();
        BindGameManager();
        Container.BindInstance<GameObject>(_prefabView);

        Container.BindInterfacesAndSelfTo<ViewFactory>()
            .AsSingle()
            .NonLazy();


        Container.BindInterfacesAndSelfTo<ViewService>()
            .AsSingle()
            .NonLazy();

        

        BindPresenters();
    }

    private void BindInput()
    {

        Container.Bind<InputService>()
            .AsSingle();
    }
    private void BindConfig()
    {
        Container.BindInstance(_configsServiceConfig).AsSingle();
        Container.Bind<ConfigsService>().AsSingle();
    }

    private void BindGameManager()
    {
        Container.BindInterfacesAndSelfTo<GameManager>()
            .AsSingle()
            .NonLazy();

        Container.Bind<CatService>()
            .FromComponentInHierarchy()
            .AsSingle();
    }

    private void BindPresenters()
    {
        Container.BindInterfacesAndSelfTo<StartViewPresenter>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<RestartViewPresenter>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<AbsenceKeyViewPresenter>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<ReceiveKeyViewPresenter>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<GameOverViewPresenter>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<LivesViewPresenter>()
            .AsSingle()
            .NonLazy();

        Container.BindInterfacesAndSelfTo<CoinCounterViewPresenter>()
            .AsSingle()
            .NonLazy();
    }
}