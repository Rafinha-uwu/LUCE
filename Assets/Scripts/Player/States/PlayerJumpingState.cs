﻿using UnityEngine;

[CreateAssetMenu(menuName = "PlayerController/PlayerJumpingState")]
public class PlayerJumpingState : PlayerState
{
    [SerializeField] private float _jumpPower;
    private static readonly string ANIMATOR_PARAMETER = "IsJumping";


    public override void EnterState(PlayerController player)
    {
        player.InputHandler.ClearJumpBuffer();
        Jump(player);
        player.Animator.SetBool(ANIMATOR_PARAMETER, true);
    }

    public override void UpdateState(PlayerController player)
    {
        player.MovingState.Move(player, playSound: false);

        if (player.GroundCheck.IsGrounded)
        {
            player.FallingState.PlayGroundHitSound(player);
            player.TransitionToState(player.MovingState);
        }
    }

    public override void ExitState(PlayerController player)
    {
        player.Animator.SetBool(ANIMATOR_PARAMETER, false);
    }


    public void Jump(PlayerController player)
    {
        player.Rb.velocity = new Vector2(player.Rb.velocity.x, _jumpPower);
        PlayJumpSound(player);
    }


    private void PlayJumpSound(PlayerController player)
    {
        FMODManager.Instance.PlayOneShotAttached(
            FMODManager.Instance.EventDatabase.PlayerJump,
            player.gameObject
        );
    }
}
