using UnityEngine;

[CreateAssetMenu(menuName = "PlayerController/PlayerFallingState")]
public class PlayerFallingState : PlayerState
{
    [SerializeField] private float _coyoteTime;
    private float _coyoteTimeCounter;


    public override void EnterState(PlayerController player)
    {
        _coyoteTimeCounter = _coyoteTime;
    }

    public override void UpdateState(PlayerController player)
    {
        _coyoteTimeCounter -= Time.fixedDeltaTime;
        player.MovingState.Move(player, playSound: false);

        if (player.GroundCheck.IsGrounded)
        {
            player.TransitionToState(player.MovingState);
        }
        else if (_coyoteTimeCounter > 0 && player.InputHandler.JumpBufferCounter > 0)
        {
            player.TransitionToState(player.JumpingState);
        }
    }

    public override void ExitState(PlayerController player)
    {
    }
}
