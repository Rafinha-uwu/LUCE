using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class PressurePlate : SwitchObject
{
    private readonly List<GameObject> _objectsOnPlate = new();


    protected override void Start()
    {
        IsOn = false;
        base.Start();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_objectsOnPlate.Contains(collision.gameObject)) return;

        _objectsOnPlate.Add(collision.gameObject);
        TurnOn();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _objectsOnPlate.Remove(collision.gameObject);
        if (_objectsOnPlate.Count == 0) TurnOff();
    }
}
