using FMODUnity;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LightMove : MonoBehaviour
{
    private static readonly string PLAYER_TAG = "Player";
    private static readonly string ANIMATOR_PARAMETER = "Move";

    private Animator _animator;

    private static readonly string EVENT_PARAMETER_STATE = "LightState";
    private StudioEventEmitter _lightMoveSound;
    [SerializeField] private float _soundMinDistance = 0f;
    [SerializeField] private float _soundMaxDistance = 10f;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_TAG))
        {
            _animator.SetBool(ANIMATOR_PARAMETER, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_TAG))
        {
            _animator.SetBool(ANIMATOR_PARAMETER, false);
        }
    }


    private void Start()
    {
        _lightMoveSound = GetLightMoveSound();
    }

    private StudioEventEmitter GetLightMoveSound()
    {
        return FMODManager.Instance.CreateEventEmitter(
            FMODManager.Instance.EventDatabase.MovingLight,
            gameObject,
            _soundMinDistance, _soundMaxDistance
        );
    }

    public void OnLightIdle() => PlayLightMoveSound(0);
    public void OnLightMoving() => PlayLightMoveSound(1);
    public void OnLightError() => PlayLightMoveSound(2);

    private void PlayLightMoveSound(int state)
    {
        if (_lightMoveSound.IsPlaying()) _lightMoveSound.Stop();
        _lightMoveSound.Play();
        FMODManager.Instance.AttachInstance(_lightMoveSound.EventInstance, transform);
        _lightMoveSound.SetParameter(EVENT_PARAMETER_STATE, state);
    }
}