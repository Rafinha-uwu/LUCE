﻿using UnityEngine;

[RequireComponent(typeof(InputHandler))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundCheck))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Flip))] // Needed for PlayerPushPullState 
public class PlayerController : MonoBehaviour
{
    public InputHandler InputHandler { get; private set; }
    public Rigidbody2D Rb { get; private set; }
    public GroundCheck GroundCheck { get; private set; }
    public Animator Animator { get; private set; }

    private PlayerState _currentState;

    [field: SerializeField] public PlayerMovingState MovingState { get; private set; }
    [field: SerializeField] public PlayerFallingState FallingState { get; private set; }
    [field: SerializeField] public PlayerJumpingState JumpingState { get; private set; }
    [field: SerializeField] public PlayerPushPullState PushPullState { get; private set; }


    private void Awake()
    {
        InputHandler = GetComponent<InputHandler>();
        Rb = GetComponent<Rigidbody2D>();
        GroundCheck = GetComponent<GroundCheck>();
        Animator = GetComponent<Animator>();
    }

    private void Start()
    {
        TransitionToState(MovingState);
    }


    public void TransitionToState(PlayerState state)
    {
        if (_currentState != null) _currentState.ExitState(this);
        _currentState = state;
        _currentState.EnterState(this);
    }

    private void FixedUpdate()
    {
        _currentState.UpdateState(this);
    }
}
