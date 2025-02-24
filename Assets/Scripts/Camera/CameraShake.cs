using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    [SerializeField] private float globalShakeForce = 1f;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public void CameraShaking (CinemachineImpulseSource impulseSource)
    {
        impulseSource.GenerateImpulseWithForce(globalShakeForce);
        FMODManager.Instance.PlayOneShot(FMODManager.Instance.EventDatabase.ScreenShake);
    }
}
