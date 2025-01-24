using FMODUnity;
using UnityEngine;

public class KeyDoor : MonoBehaviour, IDoorSound
{
    private static readonly string ANIMATOR_PARAMETER = "Open";
    private Animator _animator;

    private StudioEventEmitter _doorSound;
    [SerializeField] private float _soundMinDistance = 0f;
    [SerializeField] private float _soundMaxDistance = 10f;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool hasKey = collision.TryGetComponent(out Key key);
        if (!hasKey) return;

        _animator.SetBool(ANIMATOR_PARAMETER, true);
        PlayUnlockSound();
        key.Use();
    }


    private void Start()
    {
        _doorSound = GetDoorSound();
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
}
