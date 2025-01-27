using UnityEngine;

public class StartAnimationBehavior : StateMachineBehaviour
{
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool hasStartMenu = animator.TryGetComponent<StartMenu>(out var startMenu);
        if (!hasStartMenu) return;

        startMenu.OnAnimationEnd();
    }
}
