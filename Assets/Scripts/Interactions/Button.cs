using FMODUnity;
using UnityEngine;

public class Button : Lever
{
    [SerializeField] private float _pressTime;
    private float _pressCount = 0;

    protected override void Awake()
    {
        IsOn = false;
        base.Awake();
    }

    protected override void TurnOn()
    {
        base.TurnOn();
        _pressCount = _pressTime;
    }

    protected override StudioEventEmitter GetSwitchSound()
    {
        return FMODManager.Instance.CreateEventEmitter(
            FMODManager.Instance.EventDatabase.Button,
            gameObject,
            _soundMinDistance, _soundMaxDistance
        );
    }


    private void FixedUpdate()
    {
        if (_pressCount <= 0) return;

        _pressCount -= Time.fixedDeltaTime;
        if (_pressCount <= 0) TurnOff();
    }


    protected override void OnInteractAction()
    {
        if (_isPlayerNearby) TurnOn();
    }


    public override object GetSaveData() => false; // Start always off
}
