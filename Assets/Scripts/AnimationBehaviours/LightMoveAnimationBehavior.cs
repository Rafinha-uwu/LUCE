using UnityEngine;

public class LightMoveAnimationBehavior : StateMachineBehaviour
{
    private static readonly string MOVING_STATE_NAME = "Move";
    private static readonly string ERROR_STATE_NAME = "Stop";
    private static readonly string IDLE_STATE_NAME = "Idle";


    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool hasLightMove = animator.TryGetComponent<LightMove>(out var lightMove);
        if (!hasLightMove) return;
        
        if (stateInfo.IsName(IDLE_STATE_NAME)) lightMove.OnLightIdle();
        else if (stateInfo.IsName(MOVING_STATE_NAME)) lightMove.OnLightMoving();
        else if (stateInfo.IsName(ERROR_STATE_NAME)) lightMove.OnLightError();
    }
}
