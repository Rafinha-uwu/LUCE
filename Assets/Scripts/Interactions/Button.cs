using Unity.VisualScripting;
using UnityEngine;

public class Button : Lever
{
    [SerializeField] private float _pressTime;
    private float _pressCount = 0;

    protected override void Start()
    {
        IsOn = false;
        base.Start();
        Sprite.GetComponent<SpriteRenderer>().sprite = Off;

    }

    protected override void TurnOn()
    {
        base.TurnOn();
        _pressCount = _pressTime;
    }


    private void FixedUpdate()
    {
        if (_pressCount <= 0) return;

        _pressCount -= Time.fixedDeltaTime;
        if (_pressCount <= 0) { TurnOff(); Sprite.GetComponent<SpriteRenderer>().sprite = Off; }
        }


    protected override void OnInteractAction()
    {
        if (_isPlayerNearby) { TurnOn(); Sprite.GetComponent<SpriteRenderer>().sprite = On; }
    }
}
