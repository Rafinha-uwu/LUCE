﻿using System.Linq;
using UnityEngine;

public class SwitchWithRequirements : SwitchObject
{
    [SerializeField] private Requirement[] _requirements;

    protected virtual void Awake()
    {
        foreach (var req in _requirements) req.Switch.OnStateChange += OnSwitchStateChange;
    }

    protected virtual void OnDestroy()
    {
        foreach (var req in _requirements) req.Switch.OnStateChange -= OnSwitchStateChange;
    }

    private bool AllRequirementsMet() => _requirements.All(req => req.IsMet);

    private void OnSwitchStateChange(SwitchObject switchObject, bool isOn)
    {
        bool allRequirementsMet = AllRequirementsMet();

        if (allRequirementsMet) TurnOn();
        else TurnOff();
    }

    private void OnDrawGizmosSelected()
    {
        foreach (var req in _requirements)
        {
            if (req.Switch == null) continue;
            Gizmos.color = req.IsMet ? Color.green : Color.red;
            Gizmos.DrawLine(transform.position, req.Switch.transform.position);
        }
    }

    // Add a public method to update RequiredState
    public void SetRequiredState(int index, bool newState)
    {
        if (index >= 0 && index < _requirements.Length)
        {
            _requirements[index].RequiredState = newState;
        }
        else
        {
            Debug.LogWarning("Invalid requirement index!");
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
