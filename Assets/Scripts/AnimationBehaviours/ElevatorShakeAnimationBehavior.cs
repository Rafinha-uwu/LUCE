using FMOD.Studio;
using UnityEngine;

public class ElevatorShakeAnimationBehavior : StateMachineBehaviour
{
    private static EventInstance? _shakeSound;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_shakeSound.HasValue || !_shakeSound.Value.isValid())
            _shakeSound = GetShakeSound();

        _shakeSound?.start();
        if (_shakeSound.HasValue) FMODManager.Instance.AttachInstance(_shakeSound.Value, animator.transform);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _shakeSound?.stop(STOP_MODE.ALLOWFADEOUT);
    }


    private EventInstance? GetShakeSound() => FMODManager.Instance.CreateEventInstance(FMODManager.Instance.EventDatabase.ElevatorShake);
}
