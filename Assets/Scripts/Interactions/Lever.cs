using FMODUnity;
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

    private static readonly string EVENT_PARAMETER_IS_ON = "IsOn";
    protected StudioEventEmitter _switchSound;
    [SerializeField] protected float _soundMinDistance = 0f;
    [SerializeField] protected float _soundMaxDistance = 10f;

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
        if (_spriteRenderer != null)
            _spriteRenderer.sprite = isOn ? _spriteOn : _spriteOff;

        _switchSound.Play();
        FMODManager.Instance.AttachInstance(_switchSound.EventInstance, transform);
        _switchSound.SetParameter(EVENT_PARAMETER_IS_ON, isOn ? 1 : 0);
    }


    protected override void Start()
    {
        _switchSound = GetSwitchSound();
        base.Start();
    }

    protected virtual StudioEventEmitter GetSwitchSound()
    {
        return FMODManager.Instance.CreateEventEmitter(
            FMODManager.Instance.EventDatabase.Lever,
            gameObject,
            _soundMinDistance, _soundMaxDistance
        );
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
