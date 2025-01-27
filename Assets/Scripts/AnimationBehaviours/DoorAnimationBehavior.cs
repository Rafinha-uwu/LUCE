using UnityEngine;

public class DoorAnimationBehavior : StateMachineBehaviour
{
    [SerializeField] private bool _playOnEnter = true;
    [SerializeField] private bool _stopOnExit = true;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_playOnEnter) return;

        bool hasDoorSound = animator.TryGetComponent<IDoorSound>(out var doorSound);
        if (!hasDoorSound) return;

        doorSound.PlayDoorSound();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_stopOnExit) return;

        bool hasDoorSound = animator.TryGetComponent<IDoorSound>(out var doorSound);
        if (!hasDoorSound) return;

        doorSound.StopDoorSound();
    }
}
