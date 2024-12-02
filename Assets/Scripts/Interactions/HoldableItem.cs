using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HoldableItem : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private Transform _lastParent;


    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void StartHold(Transform holdPosition)
    {
        _lastParent = transform.parent;
        transform.SetParent(holdPosition);
        transform.position = holdPosition.position;

        _rigidbody2D.simulated = false;
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.angularVelocity = 0;
    }

    public void StopHold()
    {
        transform.SetParent(_lastParent);

        _rigidbody2D.simulated = true;
    }
}
