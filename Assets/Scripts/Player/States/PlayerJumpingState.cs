using UnityEngine;

[CreateAssetMenu(menuName = "PlayerController/PlayerJumpingState")]
public class PlayerJumpingState : PlayerState
{
    [SerializeField] private float _jumpPower;


    public override void EnterState(PlayerController player)
    {
        player.InputHandler.ClearJumpBuffer();
        Jump(player);
        player.Animator.SetBool("IsJumping", true);
    }

    public override void UpdateState(PlayerController player)
    {
        player.MovingState.Move(player);

        if (player.GroundCheck.IsGrounded)
        {
            player.TransitionToState(player.MovingState);
        }
    }

    public override void ExitState(PlayerController player)
    {
        player.Animator.SetBool("IsJumping", false);
    }


    public void Jump(PlayerController player)
    {
        player.Rb.velocity = new Vector2(player.Rb.velocity.x, _jumpPower);
    }
}
