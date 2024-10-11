using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D _rb;

    [Header("Movement Settings")]
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _jumpBuffer;
    [SerializeField] private float _coyoteTime;

    [Header("Ground Check Settings")]
    [SerializeField] private Vector2 _groundCheckSize;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    private bool _isFacingRight = true;
    private bool _isGrounded = false;
    private float _horizontalInput = 0f;
    private float _jumpBufferCounter = 0f;
    private float _coyoteTimeCounter = 0f;


    private void FixedUpdate()
    {
        HandleCounters();
        GroundCheck();
        HandleMovement();
        HandleFlip();
        HandleJump();
    }

    private void HandleCounters()
    {
        _jumpBufferCounter -= Time.fixedDeltaTime;
        _coyoteTimeCounter -= Time.fixedDeltaTime;
    }

    private void GroundCheck()
    {
        _isGrounded = Physics2D.OverlapBox(_groundCheck.position, _groundCheckSize, 0, _groundLayer);
        if (_isGrounded) _coyoteTimeCounter = _coyoteTime;
    }

    private void HandleMovement()
    {
        _rb.velocity = new Vector2(_horizontalInput * _speed, _rb.velocity.y);
    }

    private void HandleFlip()
    {
        bool isStill = _horizontalInput == 0;
        bool isMovingRight = _horizontalInput > 0;
        bool isMovingLeft = _horizontalInput < 0;

        if (isStill || isMovingRight && _isFacingRight || isMovingLeft && !_isFacingRight) return;

        _isFacingRight = !_isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void HandleJump()
    {
        if (_jumpBufferCounter <= 0 || _coyoteTimeCounter <= 0) return;

        _rb.velocity = new Vector2(_rb.velocity.x, _jumpPower);
        _jumpBufferCounter = 0;
        _coyoteTimeCounter = 0;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _horizontalInput = context.ReadValue<Vector2>().x;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed) _jumpBufferCounter = _jumpBuffer;
    }
}
