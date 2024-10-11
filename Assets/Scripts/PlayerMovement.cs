using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private float _speed;

    private bool _isFacingRight = true;
    private float _horizontalInput = 0f;


    private void FixedUpdate()
    {
        HandleMovement();
        HandleFlip();
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

    public void OnMove(InputAction.CallbackContext context)
    {
        _horizontalInput = context.ReadValue<Vector2>().x;
    }
}
