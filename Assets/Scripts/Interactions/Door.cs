using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Door : SwitchWithRequirements
{
    private bool Anim;

    protected override void Awake()
    {

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

        bool isClosed = !isOn;

        if (this.gameObject.GetComponent<Elevator>() != null)
        {
            this.gameObject.GetComponent<Elevator>().On = isOn;
        }
        if (this.gameObject.GetComponent<Code>() != null)
        {
            this.gameObject.GetComponent<Code>().On = isOn;
        }
        if (this.gameObject.GetComponent<Animator>() != null)
        {
            if (Anim == isOn) { Anim = isClosed; }
            else { Anim = isOn; };

            if (Anim == true)
            {
                this.GetComponent<Animator>().SetBool("Open", true);
            }
            else
            {
                this.GetComponent<Animator>().SetBool("Open", false);
            }
        }

    }
}
