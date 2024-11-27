using UnityEngine;

[CreateAssetMenu(menuName = "PlayerController/PlayerPushPullState")]
public class PlayerPushPullState : PlayerState
{
    [SerializeField] private float _pushPullSpeed;
    [SerializeField] private float _objectMass;
    [SerializeField] private PhysicsMaterial2D _objectMaterial;

    [SerializeField] private float _detectionRange;
    [SerializeField] private LayerMask _objectLayer;

    private PushableObject _currentPushableObject;
    private Vector2? _currentPushableObjectDirection;

    private static readonly string PLAYER_TAG = "Player";
    private static Flip _flip;


    public override void EnterState(PlayerController player)
    {
        if (_flip == null) _flip = GameObject.Find(PLAYER_TAG).GetComponent<Flip>();

        CurrentStopPushing(player);
        NewObjectCheck(player);
    }

    public override void UpdateState(PlayerController player)
    {
        Move(player);

        if (!player.GroundCheck.IsGrounded)
        {
            player.TransitionToState(player.FallingState);
            return;
        }
        else if (!player.InputHandler.PushPullAction)
        {
            player.TransitionToState(player.MovingState);
            return;
        }

        CurrentObjectCheck(player);
        NewObjectCheck(player);
    }

    public override void ExitState(PlayerController player)
    {
        CurrentStopPushing(player);
    }


    private void CurrentStopPushing(PlayerController player)
    {
        if (_currentPushableObject != null) _currentPushableObject.StopPushPull();
        _currentPushableObject = null;
        _currentPushableObjectDirection = null;

        player.Animator.SetBool("IsPushingOrPulling", false);
        _flip.enabled = true;
    }

    private void CurrentObjectCheck(PlayerController player)
    {
        if (_currentPushableObject == null) return;
        if (!_currentPushableObject.IsGrounded)
        {
            CurrentStopPushing(player);
            return;
        }

        Vector2 direction = _currentPushableObjectDirection ?? Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, direction, _detectionRange, _objectLayer);

        if (hit.collider == null || hit.collider.gameObject != _currentPushableObject.gameObject)
        {
            CurrentStopPushing(player);
            return;
        }

        // The value is negative when pulling and positive when pushing
        // If is 0, keep the previous value
        float pushPullValue = player.InputHandler.HorizontalInput * direction.x;
        if (pushPullValue != 0) player.Animator.SetBool("IsPushing", pushPullValue > 0);
    }


    private bool SetNewCurrent(PlayerController player, PushableObject pushableObject, Vector2 direction)
    {
        if (!pushableObject.IsGrounded) return false;

        _currentPushableObject = pushableObject;
        _currentPushableObjectDirection = direction;
        _currentPushableObject.StartPushPull(_objectMass, _objectMaterial);

        player.Animator.SetBool("IsPushingOrPulling", true);
        player.Animator.SetBool("IsPushing", true);
        _flip.enabled = false;
        return true;
    }

    private void NewObjectCheck(PlayerController player)
    {
        if (_currentPushableObject != null) return;

        Vector2 direction = _flip.IsFacingRight ? Vector2.right : Vector2.left;
        RaycastHit2D[] hits = Physics2D.RaycastAll(player.transform.position, direction, _detectionRange, _objectLayer);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == null) continue;

            bool hasPushableObject = hit.collider.TryGetComponent(out PushableObject pushableObject);
            if (!hasPushableObject) continue;

            bool isSet = SetNewCurrent(player, pushableObject, direction);
            if (isSet) break;
        }
    }


    public void Move(PlayerController player)
    {
        if (_currentPushableObject == null)
        {
            player.MovingState.Move(player);
            return;
        }

        player.MovingState.Move(player, _pushPullSpeed);

        float horizontalInput = player.InputHandler.HorizontalInput;
        float horizontalVelocity = horizontalInput * _pushPullSpeed;
        _currentPushableObject.Move(horizontalVelocity);
    }
}
