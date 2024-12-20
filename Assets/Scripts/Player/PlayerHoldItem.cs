﻿using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerHoldItem : MonoBehaviour
{
    [SerializeField] private Transform _holdPosition;
    [SerializeField] private float _holdRange;
    [SerializeField] private LayerMask _holdLayerMask;

    private HoldableItem _holdableItem;
    public bool IsHoldingItem => _holdableItem != null;
    
    private PlayerController _playerController;
    private static readonly string ANIMATOR_PARAMETER = "IsHolding";


    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        DropItemIfPushPullAction();
        HoldOrDropItemIfHoldAction();
    }


    private void DropItemIfPushPullAction()
    {
        if (!_playerController.InputHandler.PushPullAction) return;
        _playerController.InputHandler.ClearHoldAction();

        DropItem();
    }

    private void HoldOrDropItemIfHoldAction()
    {
        if (!_playerController.InputHandler.HoldAction) return;
        _playerController.InputHandler.ClearHoldAction();

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
            _playerController.Animator.SetBool(ANIMATOR_PARAMETER, true);
            break;
        }
    }

    private void DropItem()
    {
        if (!IsHoldingItem) return;

        _holdableItem.StopHold();
        _holdableItem = null;
        _playerController.Animator.SetBool(ANIMATOR_PARAMETER, false);
    }
    public void ForceDrop()
    {
        DropItem();
    }
}
