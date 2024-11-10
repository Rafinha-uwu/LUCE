using UnityEngine;

[RequireComponent(typeof(InputHandler))]
public class PlayerHoldItem : MonoBehaviour
{
    [SerializeField] private Transform _holdPosition;
    [SerializeField] private float _holdRange;
    [SerializeField] private LayerMask _holdLayerMask;

    private InputHandler _inputHandler;
    private HoldableItem _holdableItem;
    public bool IsHoldingItem => _holdableItem != null;


    private void Awake()
    {
        _inputHandler = GetComponent<InputHandler>();
    }

    private void FixedUpdate()
    {
        DropItemIfPushPullAction();
        HoldOrDropItemIfHoldAction();
    }


    private void DropItemIfPushPullAction()
    {
        if (!_inputHandler.PushPullAction) return;
        _inputHandler.ClearHoldAction();

        DropItem();
    }

    private void HoldOrDropItemIfHoldAction()
    {
        if (!_inputHandler.HoldAction) return;
        _inputHandler.ClearHoldAction();

        if (IsHoldingItem) DropItem();
        else HoldItem();
    }


    private void HoldItem()
    {
        if (IsHoldingItem) return;

        Vector2 size = new(2 * _holdRange, transform.localScale.y);
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, size, 0, _holdLayerMask);

        foreach (Collider2D collider in colliders)
        {
            bool hasHoldableItem = collider.TryGetComponent(out HoldableItem holdableItem);
            if (!hasHoldableItem) continue;

            _holdableItem = holdableItem;
            _holdableItem.StartHold(_holdPosition != null ? _holdPosition : transform);
            break;
        }
    }

    private void DropItem()
    {
        if (!IsHoldingItem) return;

        _holdableItem.StopHold();
        _holdableItem = null;
    }
}
