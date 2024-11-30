using UnityEngine;

[CreateAssetMenu(menuName = "PlayerController/PlayerMovingState")]
public class PlayerMovingState : PlayerState
{
    [SerializeField] public float _speed;
    private static readonly string ANIMATOR_PARAMETER = "IsMoving";


    public override void EnterState(PlayerController player)
    {
    }

    public override void UpdateState(PlayerController player)
    {
        Move(player);

        if (!player.GroundCheck.IsGrounded)
        {
            player.TransitionToState(player.FallingState);
        }
        else if (player.InputHandler.PushPullAction)
        {
            player.TransitionToState(player.PushPullState);
        }
        else if (player.InputHandler.JumpBufferCounter > 0)
        {
            player.TransitionToState(player.JumpingState);
        }
    }

    public override void ExitState(PlayerController player)
    {
    }


    public void Move(PlayerController player, float? speed = null)
    {
        float horizontalInput = player.InputHandler.HorizontalInput;
        player.Rb.velocity = new Vector2(horizontalInput * (speed ?? _speed) * Time.fixedDeltaTime, player.Rb.velocity.y);

        player.Animator.SetBool(ANIMATOR_PARAMETER, horizontalInput != 0);
    }
}
