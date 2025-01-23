using UnityEngine;

public class ElevatorAnimationBehavior : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool hasElevator = animator.TryGetComponent<Elevator>(out var elevator);
        if (!hasElevator) return;

        elevator.OnAnimationEnd();
    }
}
