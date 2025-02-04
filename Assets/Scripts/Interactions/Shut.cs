using UnityEngine;

public class Shut : SwitchObject
{
    private static readonly string PLAYER_TAG = "Player";

    public GameObject Block;
    public GameObject Black;

    private static readonly string EVENT_STATE_BLACK0 = "0";
    private static readonly string EVENT_STATE_BLACK100 = "100";
    private Animator _blackAnimator;

    [SerializeField] private Hand _hand;
    [SerializeField] private Checkpoint _shutCheckpoint;
    private Collider2D _shutCheckpointCollider;


    private void Awake()
    {
        if (_shutCheckpoint != null)
        {
            _shutCheckpointCollider = _shutCheckpoint.GetComponent<Collider2D>();
            _shutCheckpointCollider.enabled = false;
        }

        _blackAnimator = Black.GetComponent<Animator>();
        OnStateChange += OnShutStateChange;
    }

    private void OnDestroy()
    {
        OnStateChange -= OnShutStateChange;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsOn) return;
        if (!collision.CompareTag(PLAYER_TAG)) return;

        TurnOn();
    }

    private void OnShutStateChange(SwitchObject switchObject, bool isOn)
    {
        if (isOn && _hand != null) _hand.StopHand();
        if (_shutCheckpointCollider != null) _shutCheckpointCollider.enabled = isOn;

        Block.SetActive(isOn);
        _blackAnimator.Play(isOn ? EVENT_STATE_BLACK100 : EVENT_STATE_BLACK0);
    }


    public override void LoadData(object data)
    {
        TurnOff();
        base.LoadData(data);
    }
}
