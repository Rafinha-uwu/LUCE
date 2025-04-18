using System;
using UnityEngine;

public class SwitchObject : MonoBehaviour, ISavable
{
    public event Action<SwitchObject, bool> OnStateChange;
    [field: SerializeField] public bool IsOn { get; protected set; }


    protected virtual void Start() => OnStateChange?.Invoke(this, IsOn);

    protected virtual void SwitchTo(bool isOn)
    {
        if (IsOn == isOn) return;

        IsOn = isOn;
        OnStateChange?.Invoke(this, IsOn);
    }

    protected virtual void Toggle() => SwitchTo(!IsOn);
    protected virtual void TurnOn() => SwitchTo(true);
    protected virtual void TurnOff() => SwitchTo(false);


    public virtual string GetSaveName() => name;
    public virtual object GetSaveData() => IsOn;
    public virtual void LoadData(object data) => SwitchTo((bool)data);
}
