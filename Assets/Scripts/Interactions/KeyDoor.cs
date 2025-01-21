using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    private static readonly string ANIMATOR_PARAMETER = "Open";
    private Animator _animator;


    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool hasKey = collision.TryGetComponent(out Key key);
        if (!hasKey) return;

        _animator.SetBool(ANIMATOR_PARAMETER, true);
        PlayUnlockSound();
        Destroy(key.gameObject);
    }


    private void PlayUnlockSound()
    {
        FMODManager.Instance.PlayOneShotAttached(
            FMODManager.Instance.EventDatabase.KeyUnlock,
            gameObject
        );
    }
}
