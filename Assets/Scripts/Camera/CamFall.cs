using UnityEngine;

public class CamFall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb;

    private float _fallSpeedYDampingChangeTreshold;

    private void Start()
    {
        _fallSpeedYDampingChangeTreshold = CamaraManager.instance._fallSpeedYDampingChangeThreshold;
    }

    private void Update()
    {
        if (_rb.velocity.y < _fallSpeedYDampingChangeTreshold && !CamaraManager.instance.IsLerpingYDamping && !CamaraManager.instance.LerpedFromPlayerFalling)
        {
            CamaraManager.instance.LerpYDamping(true);
        }

        if (_rb.velocity.y >= 0f && !CamaraManager.instance.IsLerpingYDamping && CamaraManager.instance.LerpedFromPlayerFalling)
        {
            CamaraManager.instance.LerpedFromPlayerFalling = false;

            CamaraManager.instance.LerpYDamping(false);
        }
    }
    
}
