using UnityEngine;

[RequireComponent(typeof(GroundCheck))]
public class Key : HoldableItem
{
    private GroundCheck _groundCheck;
    private bool _isFalling = false;


    protected override void Awake()
    {
        base.Awake();
        _groundCheck = GetComponent<GroundCheck>();
    }

    private void FixedUpdate()
    {
        bool _isGrounded = _groundCheck.IsGrounded;

        // Check if the key stopped falling
        if (_isGrounded && _isFalling)
        {
            _isFalling = false;
            PlayGroundHitSound();
        }
        // Check if the key started falling
        else if (!_isGrounded && !_isFalling) _isFalling = true;
    }


    private void PlayGroundHitSound()
    {
        // Not play sound if the key is being held
        if (!_rigidbody2D.simulated) return;

        FMODManager.Instance.PlayOneShotAttached(
            FMODManager.Instance.EventDatabase.KeyGroundHit,
            gameObject
        );
    }
}
