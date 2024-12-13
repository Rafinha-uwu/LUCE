using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Lever : SwitchObject
{
    private static InputHandler _inputHandler;

    protected static readonly string PLAYER_TAG = "Player";
    protected bool _isPlayerNearby = false;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _spriteOn;
    [SerializeField] private Sprite _spriteOff;

    protected virtual void Awake()
    {
        if (_inputHandler == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag(PLAYER_TAG);
            _inputHandler = player.GetComponent<InputHandler>();
        }

        _inputHandler.OnInteractAction += OnInteractAction;
        OnStateChange += OnLeverStateChange;
    }

    protected virtual void OnDestroy()
    {
        _inputHandler.OnInteractAction -= OnInteractAction;
        OnStateChange -= OnLeverStateChange;
    }

    protected virtual void OnInteractAction()
    {
        if (_isPlayerNearby) Toggle();
    }

    protected virtual void OnLeverStateChange(SwitchObject switchObject, bool isOn)
    {
        if (_spriteRenderer == null) return;
        _spriteRenderer.sprite = isOn ? _spriteOn : _spriteOff;
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
