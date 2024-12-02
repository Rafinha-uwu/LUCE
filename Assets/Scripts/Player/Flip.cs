using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class Flip : MonoBehaviour
{
    private InputHandler InputHandler;
    private bool _isFacingRight = true;
    public bool IsFacingRight => _isFacingRight;

    public void Awake()
    {
        InputHandler = GetComponent<InputHandler>();
    }

    private void FixedUpdate()
    {
        bool isStill = InputHandler.HorizontalInput == 0;
        bool isMovingRight = InputHandler.HorizontalInput > 0;
        bool isMovingLeft = InputHandler.HorizontalInput < 0;

        if (isStill || isMovingRight && _isFacingRight || isMovingLeft && !_isFacingRight) return;

        if (_isFacingRight)
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            _isFacingRight = !_isFacingRight;
        }
        else
        {
            Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            _isFacingRight = !_isFacingRight;
        }
    }
}
