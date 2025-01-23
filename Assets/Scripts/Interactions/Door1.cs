using UnityEngine;

public class Door1 : SwitchWithRequirements
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
        if (_animator == null) return;

        if (isOn)
        {
            // Do something when switch turns ON
            _animator.SetBool("Reset", false);
            _animator.SetBool("Open", true);
        }
        else
        {
            // Do something when switch turns OFF
            _animator.SetBool("Reset", true);
            _animator.SetBool("Open", false);
        }
    }
}
