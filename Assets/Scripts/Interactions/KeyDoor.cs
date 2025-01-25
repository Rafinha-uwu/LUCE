using FMODUnity;
using UnityEngine;

public class KeyDoor : SwitchObject, IDoorSound
{
    private static readonly string ANIMATOR_PARAMETER_ON = "Open";
    private static readonly string ANIMATOR_PARAMETER_RESET = "Reset";
    private Animator _animator;

    private StudioEventEmitter _doorSound;
    [SerializeField] private float _soundMinDistance = 0f;
    [SerializeField] private float _soundMaxDistance = 10f;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
        OnStateChange += OnKeyDoorStateChange;
    }
    private void OnDestroy() => OnStateChange -= OnKeyDoorStateChange;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool hasKey = collision.TryGetComponent(out Key key);
        if (!hasKey) return;

        key.Use();
        TurnOn();
    }

    private void OnKeyDoorStateChange(SwitchObject switchObject, bool isOn)
    {
        if (isOn) PlayUnlockSound();
        if (_animator == null) return;
        _animator.SetBool(ANIMATOR_PARAMETER_RESET, false);
        _animator.SetBool(ANIMATOR_PARAMETER_ON, isOn);
    }


    protected override void Start()
    {
        _doorSound = GetDoorSound();
        base.Start();
    }

    private void PlayUnlockSound()
    {
        FMODManager.Instance.PlayOneShotAttached(
            FMODManager.Instance.EventDatabase.KeyUnlock,
            gameObject
        );
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
        _doorSound.Play();
        FMODManager.Instance.AttachInstance(_doorSound.EventInstance, transform);
    }

    public void StopDoorSound() => _doorSound.Stop();


    public override void LoadData(object data)
    {
        TurnOff();
        if (_animator != null) _animator.SetBool(ANIMATOR_PARAMETER_RESET, true);
        base.LoadData(data);
    }
}
