using FMOD.Studio;
using FMODUnity;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HoldableItem : MonoBehaviour
{
    protected Rigidbody2D _rigidbody2D;
    protected Transform _lastParent;

    protected EventInstance? _grabSound;
    [SerializeField] protected ItemType _itemType;


    protected virtual void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public virtual void StartHold(Transform holdPosition)
    {
        _lastParent = transform.parent;
        transform.SetParent(holdPosition);
        transform.SetPositionAndRotation(holdPosition.position, Quaternion.identity);

        _rigidbody2D.simulated = false;
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.angularVelocity = 0;

        PlayGrabSound();
    }

    public virtual void StopHold()
    {
        transform.SetParent(_lastParent);

        _rigidbody2D.simulated = true;
    }


    protected virtual void Start()
    {
        _grabSound = GetGrabSound();
    }

    protected virtual void PlayGrabSound()
    {
        _grabSound?.start();
    }

    protected virtual EventInstance? GetGrabSound()
    {
        EventReference? eventReference = _itemType switch
        {
            ItemType.Item => FMODManager.Instance.EventDatabase.KeyPickup,
            ItemType.Teddy => FMODManager.Instance.EventDatabase.TeddyPickup,
            ItemType.Key => FMODManager.Instance.EventDatabase.KeyPickup,
            ItemType.Polaroid => FMODManager.Instance.EventDatabase.PolaroidPickup,
            ItemType.PolaroidNarrative1 => FMODManager.Instance.EventDatabase.Narrative1Pickup,
            // ...
            _ => null
        };

        return eventReference.HasValue ? FMODManager.Instance.CreateEventInstance(eventReference.Value) : null;
    }


    public enum ItemType
    {
        Item,
        Teddy,
        Key,
        Polaroid,
        PolaroidNarrative1,
        //...
    }
}
