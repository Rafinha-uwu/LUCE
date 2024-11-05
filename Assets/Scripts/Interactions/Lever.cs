using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Lever : SwitchObject
{
    private static InputHandler _inputHandler;
    private SpriteRenderer _spriteRenderer;

    protected static readonly string PLAYER_TAG = "Player";
    protected bool _isPlayerNearby = false;


    protected virtual void Awake()
    {
        if (_inputHandler == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
            _inputHandler = player.GetComponent<InputHandler>();
        }

        _spriteRenderer = GetComponent<SpriteRenderer>();
        _inputHandler.OnInteractAction += OnInteractAction;
    }

    protected override void Start()
    {
        base.Start();
        _spriteRenderer.color = IsOn ? Color.green : Color.red;
    }

    protected override void SwitchTo(bool isOn)
    {
        base.SwitchTo(isOn);
        _spriteRenderer.color = isOn ? Color.green : Color.red;
    }

    protected virtual void OnInteractAction()
    {
        if (_isPlayerNearby) Toggle();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_isPlayerNearby) return;
        if (collision.CompareTag(PLAYER_TAG)) _isPlayerNearby = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!_isPlayerNearby) return;
        if (collision.CompareTag(PLAYER_TAG)) _isPlayerNearby = false;
    }
}
