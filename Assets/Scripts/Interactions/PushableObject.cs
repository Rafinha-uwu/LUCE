using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundCheck))]
[RequireComponent(typeof(Collider2D))]
public class PushableObject : MonoBehaviour
{
    private Rigidbody2D _rb;
    private GroundCheck _groundCheck;
    private Collider2D _collider;

    private PhysicsMaterial2D _lastMaterial;
    private float _lastMass;
    public bool IsGrounded => _groundCheck.IsGrounded;


    private void Awake()
    {
        _groundCheck = GetComponent<GroundCheck>();
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    public void StartPushPull(float newMass, PhysicsMaterial2D newMaterial)
    {
        _lastMaterial = _rb.sharedMaterial;
        _lastMass = _rb.mass;

        _rb.mass = newMass;
        _rb.sharedMaterial = newMaterial;
        _collider.sharedMaterial = newMaterial;
    }

    public void StopPushPull()
    {
        _rb.mass = _lastMass;
        _rb.sharedMaterial = _lastMaterial;
        _collider.sharedMaterial = _lastMaterial;
        Move(0f);
    }

    public void Move(float horizontalVelocity)
    {
        _rb.velocity = new Vector2(horizontalVelocity * Time.fixedDeltaTime, _rb.velocity.y);
    }
}
