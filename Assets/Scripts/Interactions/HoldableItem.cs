﻿using FMOD.Studio;
using FMODUnity;
using Newtonsoft.Json;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HoldableItem : MonoBehaviour, ISavable
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
        transform.position = holdPosition.position;
        transform.localRotation = Quaternion.identity;

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
            ItemType.PolaroidNarrative2 => FMODManager.Instance.EventDatabase.Narrative2Pickup,
            ItemType.PolaroidNarrative3 => FMODManager.Instance.EventDatabase.Narrative3Pickup,
            ItemType.PolaroidNarrative4 => FMODManager.Instance.EventDatabase.Narrative4Pickup,
            ItemType.Camera => FMODManager.Instance.EventDatabase.CameraPickup,
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
        PolaroidNarrative2,
        PolaroidNarrative3,
        PolaroidNarrative4,
        Camera,
        //...
    }


    public virtual string GetSaveName() => name;
    public virtual object GetSaveData() => new float[] { transform.position.x, transform.position.y };
    public virtual void LoadData(object data)
    {
        float[] position = JsonConvert.DeserializeObject<float[]>(data.ToString());
        transform.position = new Vector3(position[0], position[1], transform.position.z);
        
        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.angularVelocity = 0;
    }
}
