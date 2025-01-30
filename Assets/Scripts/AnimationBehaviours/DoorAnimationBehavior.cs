using UnityEngine;

public class DoorAnimationBehavior : StateMachineBehaviour
{
    [SerializeField] private bool _playOnEnter = true;
    [SerializeField] private bool _stopOnExit = true;
    [SerializeField] private string _data = null;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_playOnEnter) return;

        bool hasDoorSound = animator.TryGetComponent<IDoorSound>(out var doorSound);
        if (!hasDoorSound) return;

        doorSound.PlayDoorSound(_data == string.Empty ? null : _data);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_stopOnExit) return;

        bool hasDoorSound = animator.TryGetComponent<IDoorSound>(out var doorSound);
        if (!hasDoorSound) return;

        doorSound.StopDoorSound(_data == string.Empty ? null : _data);
    }
}
