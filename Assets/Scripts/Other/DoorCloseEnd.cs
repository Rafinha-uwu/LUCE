using UnityEngine;

public class DoorCloseEnd : SwitchObject
{
    private static readonly string PLAYER_TAG = "Player";

    private static readonly string EVENT_STATE_OPEN = "OpenDoor";
    private static readonly string EVENT_STATE_CLOSE = "CloseDoor";
    private Animator _animator;


    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        OnStateChange += OnDoorCloseEndStateChange;
    }

    private void OnDestroy()
    {
        OnStateChange -= OnDoorCloseEndStateChange;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsOn) return;
        if (!collision.CompareTag(PLAYER_TAG)) return;

        TurnOn();
    }

    private void OnDoorCloseEndStateChange(SwitchObject switchObject, bool isOn)
    {
        if (_animator != null) _animator.Play(isOn ? EVENT_STATE_CLOSE : EVENT_STATE_OPEN);
    }
}
