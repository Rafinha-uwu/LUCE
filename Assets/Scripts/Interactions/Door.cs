using FMODUnity;
using UnityEngine;

public class Door : SwitchWithRequirements, IDoorSound
{
    private static readonly string ANIMATOR_PARAMETER_ON = "Open";
    private static readonly string ANIMATOR_PARAMETER_RESET = "Reset";
    private Animator _animator;

    private StudioEventEmitter _doorSound;
    [SerializeField] private bool _playSound = true;
    [SerializeField] private float _soundMinDistance = 0f;
    [SerializeField] private float _soundMaxDistance = 10f;


    protected override void Awake()
    {
        _animator = GetComponent<Animator>();
        OnStateChange += OnDoorStateChange;
        IsOn = false;
        base.Awake();
    }

    protected override void OnDestroy()
    {
        OnStateChange -= OnDoorStateChange;
        base.OnDestroy();
    }


    private void OnDoorStateChange(SwitchObject switchObject, bool isOn)
    {
        if (_animator == null) return;
        _animator.SetBool(ANIMATOR_PARAMETER_RESET, false);
        _animator.SetBool(ANIMATOR_PARAMETER_ON, isOn);
    }


    protected override void Start()
    {
        base.Start();
        _doorSound = GetDoorSound();
    }

    private StudioEventEmitter GetDoorSound()
    {
        return FMODManager.Instance.CreateEventEmitter(
            FMODManager.Instance.EventDatabase.Door,
            gameObject,
            _soundMinDistance, _soundMaxDistance
        );
    }

    public void PlayDoorSound()
    {
        if (!_playSound) return;

        _doorSound.Play();
        FMODManager.Instance.AttachInstance(_doorSound.EventInstance, transform);
    }

    public void StopDoorSound() => _doorSound.Stop();


    public override void LoadData(object data)
    {
        TurnOff();
        if (_animator != null) _animator.SetBool(ANIMATOR_PARAMETER_RESET, true);
    }
}
