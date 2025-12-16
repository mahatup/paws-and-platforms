using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputService
{
    private InputConfig _config;

    public InputService(InputConfig config)
    {
        _config = config;
    }

    public float MoveAxis => Input.GetAxis(_config.Axis);
    public bool IsJumpPressed => Input.GetKeyDown(_config.JumpKey);
    public bool IsSkipPressed => Input.GetKeyDown(_config.SkipKey);

}
