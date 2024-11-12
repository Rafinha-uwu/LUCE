using UnityEngine;

[CreateAssetMenu(menuName = "PlayerController/PlayerMovingState")]
public class PlayerMovingState : PlayerState
{
    [SerializeField] public float _speed;


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


    public void Move(PlayerController player)
    {
        float horizontalInput = player.InputHandler.HorizontalInput;
        player.Rb.velocity = new Vector2(horizontalInput * _speed, player.Rb.velocity.y);
    }
}
