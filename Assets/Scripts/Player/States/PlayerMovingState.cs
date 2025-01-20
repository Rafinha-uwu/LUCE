using FMODUnity;
using UnityEngine;

[CreateAssetMenu(menuName = "PlayerController/PlayerMovingState")]
public class PlayerMovingState : PlayerState
{
    [SerializeField] public float _speed;
    private static readonly string ANIMATOR_PARAMETER = "IsMoving";

    private StudioEventEmitter _playerMoveSound = null;
    [SerializeField] private float _soundMinDistance = 0f;
    [SerializeField] private float _soundMaxDistance = 10f;


    public override void EnterState(PlayerController player)
    {
        if (_playerMoveSound != null) return;

        _playerMoveSound = FMODManager.Instance.CreateEventEmitter(
            FMODManager.Instance.EventDatabase.PlayerMove,
            player.gameObject,
            _soundMinDistance, _soundMaxDistance
        );
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


    public void Move(PlayerController player, float? speed = null, bool playSound = true)
    {
        float horizontalInput = player.InputHandler.HorizontalInput;
        player.Rb.velocity = new Vector2(horizontalInput * (speed ?? _speed) * Time.fixedDeltaTime, player.Rb.velocity.y);

        player.Animator.SetBool(ANIMATOR_PARAMETER, horizontalInput != 0);

        PlayMoveSound(player, horizontalInput, playSound);
    }


    private void PlayMoveSound(PlayerController player, float horizontalInput, bool playSound = true)
    {
        if (_playerMoveSound == null) return;

        // Stop the sound if the player is not moving
        // or the sound is not supposed to be played
        if (!playSound || horizontalInput == 0) _playerMoveSound.Stop();

        // Start the sound if it is not playing
        else if (!_playerMoveSound.IsPlaying())
        {
            _playerMoveSound.Play();
            FMODManager.Instance.AttachInstance(_playerMoveSound.EventInstance, player.transform, player.Rb);
        }
    }
}
