using FMODUnity;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PressurePlate : SwitchObject
{
    private readonly List<GameObject> _objectsOnPlate = new();

    private static readonly string ANIMATOR_PARAMETER = "press";
    private Animator _animator;

    private static readonly string EVENT_PARAMETER_IS_ON = "IsOn";
    private StudioEventEmitter _switchSound;
    [SerializeField] private float _soundMinDistance = 0f;
    [SerializeField] private float _soundMaxDistance = 10f;

    protected void Awake()
    {
        _animator = GetComponent<Animator>();
        OnStateChange += OnPressureStateChange;
    }

    protected void OnDestroy()
    {
        OnStateChange -= OnPressureStateChange;
    }

    protected override void Start()
    {
        IsOn = false;
        _switchSound = GetSwitchSound();
        base.Start();
    }

    private StudioEventEmitter GetSwitchSound()
    {
        return FMODManager.Instance.CreateEventEmitter(
            FMODManager.Instance.EventDatabase.PressurePlate,
            gameObject,
            _soundMinDistance, _soundMaxDistance
        );
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_objectsOnPlate.Contains(collision.gameObject)) return;
        _objectsOnPlate.Add(collision.gameObject);
        TurnOn();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _objectsOnPlate.Remove(collision.gameObject);
        if (_objectsOnPlate.Count == 0) TurnOff();
    }

    private void OnPressureStateChange(SwitchObject switchObject, bool isOn)
    {
        if (_animator != null)
            _animator.SetBool(ANIMATOR_PARAMETER, isOn);

        _switchSound.Play();
        FMODManager.Instance.AttachInstance(_switchSound.EventInstance, transform);
        _switchSound.SetParameter(EVENT_PARAMETER_IS_ON, isOn ? 1 : 0);
    }
}
