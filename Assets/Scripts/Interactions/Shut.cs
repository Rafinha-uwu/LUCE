using UnityEngine;

public class Shut : SwitchObject
{
    private static readonly string PLAYER_TAG = "Player";

    public GameObject Block;
    public GameObject Black;

    private static readonly string EVENT_PARAMETER_BLACK0 = "0";
    private static readonly string EVENT_PARAMETER_BLACK100 = "100";
    private Animator _blackAnimator;


    private void Awake()
    {
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
        Block.SetActive(isOn);
        _blackAnimator.Play(isOn ? EVENT_PARAMETER_BLACK100 : EVENT_PARAMETER_BLACK0);
    }
}
