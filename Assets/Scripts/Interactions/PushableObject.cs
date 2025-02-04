using FMODUnity;
using Newtonsoft.Json;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(GroundCheck))]
[RequireComponent(typeof(Collider2D))]
public class PushableObject : MonoBehaviour, ISavable
{
    private Rigidbody2D _rb;
    private GroundCheck _groundCheck;
    private Collider2D _collider;

    private PhysicsMaterial2D _lastMaterial;
    private float _lastMass;
    public bool IsGrounded => _groundCheck.IsGrounded;

    private StudioEventEmitter _objectMoveSound;
    [SerializeField] private float _soundMinDistance = 0f;
    [SerializeField] private float _soundMaxDistance = 10f;
    private bool _isFalling = false;


    private void Awake()
    {
        _groundCheck = GetComponent<GroundCheck>();
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }

    private void Start()
    {
        _objectMoveSound = FMODManager.Instance.CreateEventEmitter(
            FMODManager.Instance.EventDatabase.ObjectMove,
            gameObject,
            _soundMinDistance, _soundMaxDistance
        );
    }


    public void StartPushPull(float newMass, PhysicsMaterial2D newMaterial)
    {
        _lastMaterial = _rb.sharedMaterial;
        _lastMass = _rb.mass;

        _rb.mass = newMass;
        _rb.sharedMaterial = newMaterial;
        _collider.sharedMaterial = newMaterial;
    }

    public void StopPushPull()
    {
        _rb.mass = _lastMass;
        _rb.sharedMaterial = _lastMaterial;
        _collider.sharedMaterial = _lastMaterial;
        Move(0f);
    }


    public void Move(float horizontalVelocity)
    {
        _rb.velocity = new Vector2(horizontalVelocity * Time.fixedDeltaTime, _rb.velocity.y);
        PlayMoveSound(horizontalVelocity);
    }

    private void FixedUpdate()
    {
        // Check if the object stopped falling
        if (IsGrounded && _isFalling)
        {
            _isFalling = false;
            PlayGroundHitSound();
        }
        // Check if the object started falling
        else if (!IsGrounded && !_isFalling) _isFalling = true;
    }


    private void PlayMoveSound(float horizontalVelocity)
    {
        if (_objectMoveSound == null) return;

        // Stop the sound if the object is not moving
        if (horizontalVelocity == 0) _objectMoveSound.Stop();

        // Start the sound if it is not playing
        else if (!_objectMoveSound.IsPlaying())
        {
            _objectMoveSound.Play();
            FMODManager.Instance.AttachInstance(_objectMoveSound.EventInstance, transform, _rb);
        }
    }

    private void PlayGroundHitSound()
    {
        FMODManager.Instance.PlayOneShotAttached(
            FMODManager.Instance.EventDatabase.ObjectGroundHit,
            gameObject
        );
    }


    public string GetSaveName() => name;
    public object GetSaveData() => new float[] { transform.position.x, transform.position.y };
    public void LoadData(object data)
    {
        float[] position = JsonConvert.DeserializeObject<float[]>(data.ToString());
        transform.position = new Vector3(position[0], position[1], transform.position.z);

        _rb.velocity = Vector2.zero;
        _rb.angularVelocity = 0;
    }
}
