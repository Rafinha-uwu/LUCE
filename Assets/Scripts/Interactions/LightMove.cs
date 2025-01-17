using UnityEngine;

[RequireComponent(typeof(Animator))]
public class LightMove : MonoBehaviour
{
    private static readonly string PLAYER_TAG = "Player";
    private static readonly string ANIMATOR_PARAMETER = "Move";

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_TAG))
        {
            _animator.SetBool(ANIMATOR_PARAMETER, true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_TAG))
        {
            _animator.SetBool(ANIMATOR_PARAMETER, false);
        }
    }


}