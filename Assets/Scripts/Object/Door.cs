using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Door : SwitchWithRequirements
{
    private BoxCollider2D _collider;
    private ShadowCaster2D _shadowCaster;
    private SpriteRenderer _renderer;

    protected override void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _shadowCaster = GetComponent<ShadowCaster2D>();

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

        _collider.enabled = isClosed;
        _renderer.enabled = isClosed;
        if (_shadowCaster != null) _shadowCaster.enabled = isClosed;
    }
}
