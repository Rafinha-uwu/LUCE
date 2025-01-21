using UnityEngine;

public class DoorAnimationBehavior : StateMachineBehaviour
{
    [SerializeField] private bool _playOnEnter = true;
    [SerializeField] private bool _stopOnExit = true;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_playOnEnter) return;

        bool hasDoor = animator.TryGetComponent<Door>(out var door);
        if (!hasDoor) return;

        door.PlayDoorSound();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!_stopOnExit) return;

        bool hasDoor = animator.TryGetComponent<Door>(out var door);
        if (!hasDoor) return;

        door.StopDoorSound();
    }
}
