using System.Linq;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Configs
{

    //конфиг локатор
    public class ConfigsService
    {
        private ConfigsServiceConfig _config;

        [Inject]
        public ConfigsService(
            ConfigsServiceConfig config)
        {
            _config = config;
        }

        public T GetConfig<T>() where T : ScriptableObject
        {
            return (T)_config.Configs.FirstOrDefault(config => config.GetType() == typeof(T));
        }
    }
}