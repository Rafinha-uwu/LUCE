using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class Flip : MonoBehaviour
{
    private InputHandler InputHandler;
    private bool _isFacingRight = true;
    public bool IsFacingRight => _isFacingRight;

    [SerializeField] private GameObject _stableGameObject;


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

        float angle = isMovingRight ? 0 : 180f;

        Vector3 rotator = new(transform.rotation.x, angle, transform.rotation.z);
        transform.rotation = Quaternion.Euler(rotator);
        _isFacingRight = !_isFacingRight;


        if (_stableGameObject == null) return;

        rotator = new(0, 0, 0);
        _stableGameObject.transform.rotation = Quaternion.Euler(rotator);
    }
}
