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
            PlayGroundHitSound(player);
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


    public void PlayGroundHitSound(PlayerController player)
    {
        FMODManager.Instance.PlayOneShotAttached(
            FMODManager.Instance.EventDatabase.PlayerGroundHit,
            player.gameObject
        );
    }
}
