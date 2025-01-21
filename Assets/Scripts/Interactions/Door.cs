using FMODUnity;
using UnityEngine;

public class Door : SwitchWithRequirements
{
    private static readonly string ANIMATOR_PARAMETER = "Open";
    private Animator _animator;

    private StudioEventEmitter _doorSound;
    [SerializeField] private bool _playSound = true;
    [SerializeField] private float _soundMinDistance = 0f;
    [SerializeField] private float _soundMaxDistance = 10f;


    protected override void Awake()
    {
        _animator = GetComponent<Animator>();
        OnStateChange += OnDoorStateChange;
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
        _animator.SetBool(ANIMATOR_PARAMETER, isOn);
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
}
