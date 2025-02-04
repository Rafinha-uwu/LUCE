using FMODUnity;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PuzzleEyeDoors : MonoBehaviour, IDoorSound
{
    private Animator _doorsAnimator;
    [SerializeField] private Transform _doorTransformL;
    [SerializeField] private Transform _doorTransformR;

    private StudioEventEmitter _doorSoundL;
    private StudioEventEmitter _doorSoundR;
    [SerializeField] private float _soundMinDistance = 0f;
    [SerializeField] private float _soundMaxDistance = 10f;


    private void Awake()
    {
        _doorsAnimator = GetComponent<Animator>();
    }


    public void DoorStart() => _doorsAnimator.Play("Open 1");
    public void DoorUp() => _doorsAnimator.Play("Open");
    public void DoorDown() => _doorsAnimator.Play("Close");


    private void Start()
    {
        _doorSoundL = GetDoorSound(_doorTransformL);
        _doorSoundR = GetDoorSound(_doorTransformR);
    }

    private StudioEventEmitter GetDoorSound(Transform doorTransform)
    {
        return FMODManager.Instance.CreateEventEmitter(
            FMODManager.Instance.EventDatabase.Door,
            doorTransform.gameObject,
            _soundMinDistance, _soundMaxDistance
        );
    }


    public void PlayDoorSound(string data = null)
    {
        if (_doorSoundL != null && (data == null || data == "left"))
        {
            if (_doorSoundL.IsPlaying()) _doorSoundL.Stop();
            _doorSoundL.Play();
            FMODManager.Instance.AttachInstance(_doorSoundL.EventInstance, _doorTransformL);
        }
        if (_doorSoundR != null && (data == null || data == "right"))
        {
            if (_doorSoundR.IsPlaying()) _doorSoundR.Stop();
            _doorSoundR.Play();
            FMODManager.Instance.AttachInstance(_doorSoundR.EventInstance, _doorTransformR);
        }
    }

    public void StopDoorSound(string data = null)
    {
        if (_doorSoundL != null && (data == null || data == "left")) _doorSoundL.Stop();
        if (_doorSoundR != null && (data == null || data == "right")) _doorSoundR.Stop();
    }
}
