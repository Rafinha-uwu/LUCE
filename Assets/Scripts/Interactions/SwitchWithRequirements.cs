﻿using System.Linq;
using UnityEngine;

public class SwitchWithRequirements : SwitchObject
{
    [SerializeField] protected Requirement[] _requirements;

    protected virtual void Awake()
    {
        foreach (var req in _requirements) req.Switch.OnStateChange += OnSwitchStateChange;
    }

    protected virtual void OnDestroy()
    {
        foreach (var req in _requirements) req.Switch.OnStateChange -= OnSwitchStateChange;
    }

    protected virtual bool AllRequirementsMet() => _requirements.All(req => req.IsMet);

    protected virtual void OnSwitchStateChange(SwitchObject switchObject, bool isOn)
    {
        bool allRequirementsMet = AllRequirementsMet();

        if (allRequirementsMet) TurnOn();
        else TurnOff();
    }

    protected virtual void OnDrawGizmosSelected()
    {
        foreach (var req in _requirements)
        {
            if (req.Switch == null) continue;
            Gizmos.color = req.IsMet ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position, req.Switch.transform.position);
        }
    }


    [System.Serializable]
    public class Requirement
    {
        public SwitchObject Switch;
        public bool RequiredState;

        public bool IsMet => Switch.IsOn == RequiredState;
    }
}
