using FMODUnity;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class Elevator : SwitchWithRequirements
{
    private static readonly string PLAYER_TAG = "Player";
    private static readonly string ANIMATOR_PARAMETER_INSIDE = "PlayerInside";
    private static readonly string ANIMATOR_PARAMETER_NEXT = "GoToNext";
    private static readonly string ANIMATOR_PARAMETER_FLOOR = "Floor";

    private Animator _animator;
    private Light2D _childLight;
    private InputHandler _inputHandler;

    private static readonly string EVENT_PARAMETER_STATE = "ElevatorState";
    private StudioEventEmitter _elevatorSound;
    [SerializeField] private float _soundMinDistance = 0f;
    [SerializeField] private float _soundMaxDistance = 10f;

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

        // Set the initial floor
        SetCurrentFloor(0);

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

        UpdateElevatorSound(true);

        // Set the next floor as the current floor
        _animator.SetInteger(ANIMATOR_PARAMETER_FLOOR, -1);
        _animator.SetTrigger(ANIMATOR_PARAMETER_NEXT);
        SetCurrentFloor(_currentIndex + 1);

        // Stop the player from moving
        if (_inputHandler != null) _inputHandler.PauseInput(this);
    }


    protected override void OnSwitchStateChange(SwitchObject switchObject, bool isOn)
    {
        bool oldIsOn = IsOn;
        IsOn = _currentFloor.IsMet;

        if (oldIsOn == IsOn) return;

        UpdateElevatorLight();
        UpdateElevatorSound();
    }

    public void OnAnimationEnd()
    {
        // Resume the player's movement
        if (_inputHandler != null) _inputHandler.ResumeInput(this);

        // Reset the trigger
        _animator.ResetTrigger(ANIMATOR_PARAMETER_NEXT);

        OnSwitchStateChange(null, IsOn);
    }

    private void UpdateElevatorLight()
    {
        if (_childLight == null) return;
        _childLight.color = IsOn ? _onColor : _offColor;
    }


    protected override void Start()
    {
        _elevatorSound = GetElevatorSound();
        base.Start();
    }

    private StudioEventEmitter GetElevatorSound()
    {
        return FMODManager.Instance.CreateEventEmitter(
            FMODManager.Instance.EventDatabase.Elevator,
            gameObject,
            _soundMinDistance, _soundMaxDistance
        );
    }

    private void UpdateElevatorSound(bool isMoving = false)
    {
        if (_elevatorSound == null) return;

        // 0 = Off, 1 = On, 2 = Moving
        ElevatorState elevatorState = isMoving ? ElevatorState.Moving : IsOn ? ElevatorState.On : ElevatorState.Off;

        if (_elevatorSound.IsPlaying()) _elevatorSound.Stop();
        _elevatorSound.Play();
        FMODManager.Instance.AttachInstance(_elevatorSound.EventInstance, transform);
        _elevatorSound.SetParameter(EVENT_PARAMETER_STATE, (int)elevatorState);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(PLAYER_TAG)) return;

        _animator.SetInteger(ANIMATOR_PARAMETER_FLOOR, -1);
        _animator.SetBool(ANIMATOR_PARAMETER_INSIDE, true);
        if (collision.gameObject.scene.isLoaded) collision.transform.SetParent(transform);

        AutomaticMovement();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(PLAYER_TAG)) return;

        _animator.SetInteger(ANIMATOR_PARAMETER_FLOOR, -1);
        _animator.SetBool(ANIMATOR_PARAMETER_INSIDE, false);
        if (collision.gameObject.scene.isLoaded) collision.transform.SetParent(null);
    }


    public override object GetSaveData() => _currentIndex;
    public override void LoadData(object data)
    {
        int floor = JsonConvert.DeserializeObject<int>(data.ToString());
        _animator.SetInteger(ANIMATOR_PARAMETER_FLOOR, floor);
        
        SetCurrentFloor(floor);
        OnSwitchStateChange(null, IsOn);
    }

    public enum ElevatorState
    {
        Off,
        On,
        Moving
    }
}