﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class InputHandler : MonoBehaviour
{
    private PlayerInput _playerInput;
    private static readonly string PLAYER_ACTION_MAP = "Player";
    private static readonly string UI_ACTION_MAP = "UI";

    [SerializeField] private float _jumpBuffer;
    private readonly List<object> _stoppers = new();

    public float HorizontalInput { get; private set; }
    public float JumpBufferCounter { get; private set; }
    public bool PushPullAction { get; private set; }
    public bool HoldAction { get; private set; }

    public event Action OnInteractAction;
    public event Action OnPauseAction;
    public event Action OnResumeAction;


    private void Awake() => _playerInput = GetComponent<PlayerInput>();

    public void ResumeInput(object stopper)
    {
        // Remove the stopper from the list + Resume the input if there is no stoppers
        _stoppers.Remove(stopper);
        if (_stoppers.Count == 0) _playerInput.SwitchCurrentActionMap(PLAYER_ACTION_MAP);
    }

    public void PauseInput(object stopper)
    {
        // Add the stopper to the list + Pause the input + Clear the jump buffer
        _stoppers.Add(stopper);
        _playerInput.SwitchCurrentActionMap(UI_ACTION_MAP);
        ClearJumpBuffer();
    }


    public void ClearJumpBuffer()
    {
        JumpBufferCounter = 0f;
    }

    public void ClearHoldAction()
    {
        HoldAction = false;
    }

    private void FixedUpdate()
    {
        if (JumpBufferCounter > 0) JumpBufferCounter -= Time.fixedDeltaTime;
    }


    public void OnMove(InputAction.CallbackContext context)
    {
        float x = context.ReadValue<Vector2>().x;
        HorizontalInput = x == 0 ? 0 : Mathf.Sign(x);
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

    public void OnHold(InputAction.CallbackContext context)
    {
        HoldAction = context.ReadValueAsButton();
    }


    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed) OnPauseAction?.Invoke();
    }

    public void OnResume(InputAction.CallbackContext context)
    {
        if (context.performed) OnResumeAction?.Invoke();
    }
}
