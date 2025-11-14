using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputService
{
    public float Horizontal {  get; private set; }
    public bool JumpPressed { get; private set; }

    public void ReadInput()
    {
        Horizontal = Input.GetAxis("Horizontal");
        JumpPressed = Input.GetKeyDown(KeyCode.Space);
    }
}
