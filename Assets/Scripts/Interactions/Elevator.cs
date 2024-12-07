using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class Elevator : SwitchWithRequirements
{
    private static readonly string PLAYER_TAG = "Player";
    private static readonly string ANIMATOR_PARAMETER_INSIDE = "PlayerInside";
    private static readonly string ANIMATOR_PARAMETER_NEXT = "GoToNext";

    private Animator _animator;
    private Light2D _childLight;
    private InputHandler _inputHandler;

    [SerializeField] private Color _onColor;
    [SerializeField] private Color _offColor;

    private Requirement _currentFloor;
    private int _currentIndex;


    protected override void Awake()
    {
        _inputHandler = GameObject.FindGameObjectWithTag(PLAYER_TAG).GetComponent<InputHandler>();
        _childLight = GetComponentInChildren<Light2D>();
        _animator = GetComponent<Animator>();

        if (_requirements.Length == 0) throw new System.Exception("No floors (requirements) set for the elevator");

        // Set the initial floor and update the light color
        SetCurrentFloor(0);
        UpdateElevatorLight();

        base.Awake();
    }


    private void SetCurrentFloor(int index)
    {
        _currentIndex = index % _requirements.Length;
        _currentFloor = _requirements[_currentIndex];
    }

    private void AutomaticMovement()
    {
        // Check if the floor object is met
        if (!_currentFloor.IsMet) return;

        // Set the next floor as the current floor
        _animator.SetTrigger(ANIMATOR_PARAMETER_NEXT);
        SetCurrentFloor(_currentIndex + 1);

        // Stop the player from moving
        if (_inputHandler != null) _inputHandler.PauseInput();
    }


    protected override void OnSwitchStateChange(SwitchObject switchObject, bool isOn) => UpdateElevatorLight();
    public void OnAnimationEnd()
    {
        // Resume the player's movement
        if (_inputHandler != null) _inputHandler.ResumeInput();

        // Reset the trigger and update the light color
        _animator.ResetTrigger(ANIMATOR_PARAMETER_NEXT);
        UpdateElevatorLight();
    }

    private void UpdateElevatorLight()
    {
        if (_childLight == null) return;

        IsOn = _currentFloor.IsMet;
        _childLight.color = IsOn ? _onColor : _offColor;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(PLAYER_TAG)) return;

        _animator.SetBool(ANIMATOR_PARAMETER_INSIDE, true);
        if (collision.gameObject.scene.isLoaded) collision.transform.SetParent(transform);

        AutomaticMovement();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(PLAYER_TAG)) return;

        _animator.SetBool(ANIMATOR_PARAMETER_INSIDE, false);
        if (collision.gameObject.scene.isLoaded) collision.transform.SetParent(null);
    }
}