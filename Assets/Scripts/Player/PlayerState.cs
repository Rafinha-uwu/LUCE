using UnityEngine;

public abstract class PlayerState : ScriptableObject
{
    public virtual void EnterState(PlayerController player) { }
    public virtual void UpdateState(PlayerController player) { }
    public virtual void ExitState(PlayerController player) { }
}
