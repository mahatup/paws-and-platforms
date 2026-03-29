using Assets.Scripts.Configs;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay
{
    public class InputService
    {
        private InputConfig _inputConfig;

        [Inject]
        public InputService(ConfigsService configService)
        {
            _inputConfig = configService.GetConfig<InputConfig>();
        }

        public float MoveAxis => Input.GetAxis(_inputConfig.Axis);
        public bool IsJumpPressed => Input.GetKeyDown(_inputConfig.JumpKey);
        public bool IsSkipPressed => Input.GetKeyDown(_inputConfig.SkipKey);

    }
}