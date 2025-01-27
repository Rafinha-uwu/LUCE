using Newtonsoft.Json;
using UnityEngine;

[RequireComponent(typeof(GroundCheck))]
public class Key : HoldableItem
{
    private GroundCheck _groundCheck;
    private bool _isFalling = false;


    protected override void Awake()
    {
        base.Awake();
        _groundCheck = GetComponent<GroundCheck>();
    }

    private void FixedUpdate()
    {
        bool _isGrounded = _groundCheck.IsGrounded;

        // Check if the key stopped falling
        if (_isGrounded && _isFalling)
        {
            _isFalling = false;
            PlayGroundHitSound();
        }
        // Check if the key started falling
        else if (!_isGrounded && !_isFalling) _isFalling = true;
    }


    private void PlayGroundHitSound()
    {
        // Not play sound if the key is being held
        if (!_rigidbody2D.simulated) return;

        FMODManager.Instance.PlayOneShotAttached(
            FMODManager.Instance.EventDatabase.KeyGroundHit,
            gameObject
        );
    }

    public void Use() => gameObject.SetActive(false);


    public override object GetSaveData() => new KeySaveData {
        Position = new float[] { transform.position.x, transform.position.y },
        Used = !gameObject.activeSelf
    };

    public override void LoadData(object data)
    {
        KeySaveData keySaveData = JsonConvert.DeserializeObject<KeySaveData>(data.ToString());
        transform.position = new Vector3(keySaveData.Position[0], keySaveData.Position[1], transform.position.z);

        _rigidbody2D.velocity = Vector2.zero;
        _rigidbody2D.angularVelocity = 0;

        gameObject.SetActive(!keySaveData.Used);
    }

    [System.Serializable]
    public class KeySaveData
    {
        public float[] Position;
        public bool Used;
    }
}
