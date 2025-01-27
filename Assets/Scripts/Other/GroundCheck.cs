using UnityEngine;
using System.Linq;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private Vector2 _groundCheckSize;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private LayerMask _groundLayer;

    public bool IsGrounded { get; private set; }


    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(_groundCheck.position, _groundCheckSize, 0, _groundLayer);
        IsGrounded = colliders.Any(collider => collider.gameObject != gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        if (_groundCheck == null) return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(_groundCheck.position, _groundCheckSize);
    }
}
