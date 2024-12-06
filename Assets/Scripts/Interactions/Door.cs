using UnityEngine;

public class Door : SwitchWithRequirements
{
    private Animator _animator;

    protected override void Awake()
    {
        _animator = GetComponent<Animator>();
        OnStateChange += OnDoorStateChange;
        base.Awake();
    }

    protected override void OnDestroy()
    {
        OnStateChange -= OnDoorStateChange;
        base.OnDestroy();
    }


    private void OnDoorStateChange(SwitchObject switchObject, bool isOn)
    {
        if (this.gameObject.GetComponent<Elevator>() != null)
        {
            this.gameObject.GetComponent<Elevator>().On = isOn;
        }
        if (this.gameObject.GetComponent<Code>() != null)
        {
            this.gameObject.GetComponent<Code>().On = isOn;
        }

        if (_animator == null) return;
        _animator.SetBool("Open", isOn);
    }
}
