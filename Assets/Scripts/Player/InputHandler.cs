using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private float _jumpBuffer;
    
    public float HorizontalInput { get; private set; }
    public float JumpBufferCounter { get; private set; }
    public bool PushPullAction { get; private set; }

    public event Action OnInteractAction;


    public void ClearJumpBuffer()
    {
        JumpBufferCounter = 0f;
    }

    private void FixedUpdate()
    {
        if (JumpBufferCounter > 0) JumpBufferCounter -= Time.fixedDeltaTime;
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        HorizontalInput = context.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) JumpBufferCounter = _jumpBuffer;
    }

    public void OnPushPull(InputAction.CallbackContext context)
    {
        PushPullAction = context.ReadValueAsButton();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed) OnInteractAction?.Invoke();
    }
}
