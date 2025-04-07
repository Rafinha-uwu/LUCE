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

    public void OnLightIdle() => PlayLightMoveSound(LightMoveState.Idle);
    public void OnLightMoving() => PlayLightMoveSound(LightMoveState.Moving);
    public void OnLightError() => PlayLightMoveSound(LightMoveState.Error);

    private void PlayLightMoveSound(LightMoveState state)
    {
        if (_lightMoveSound == null) return;

        if (_lightMoveSound.IsPlaying()) _lightMoveSound.Stop();
        _lightMoveSound.Play();
        FMODManager.Instance.AttachInstance(_lightMoveSound.EventInstance, transform);
        _lightMoveSound.SetParameter(EVENT_PARAMETER_STATE, (int)state);
    }

    public enum LightMoveState
    {
        Idle,
        Moving,
        Error
    }
}