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

        CurrentStopPushing();
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
        CurrentStopPushing();
    }


    private void CurrentStopPushing()
    {
        if (_currentPushableObject != null) _currentPushableObject.StopPushPull();
        _currentPushableObject = null;
        _currentPushableObjectDirection = null;

        _flip.enabled = true;
    }

    private void CurrentObjectCheck(PlayerController player)
    {
        if (_currentPushableObject == null) return;
        if (!_currentPushableObject.IsGrounded) CurrentStopPushing();

        Vector2 direction = _currentPushableObjectDirection ?? Vector2.right;
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, direction, _detectionRange, _objectLayer);

        if (hit.collider == null || hit.collider.gameObject != _currentPushableObject.gameObject) CurrentStopPushing();
    }


    private bool SetNewCurrent(PushableObject pushableObject, Vector2 direction)
    {
        if (!pushableObject.IsGrounded) return false;

        _currentPushableObject = pushableObject;
        _currentPushableObjectDirection = direction;
        _currentPushableObject.StartPushPull(_objectMass, _objectMaterial);

        _flip.enabled = false;
        return true;
    }

    private void NewObjectCheck(PlayerController player)
    {
        if (_currentPushableObject != null) return;

        float horizontalInput = player.InputHandler.HorizontalInput;
        Vector2 direction = horizontalInput >= 0 ? Vector2.right : Vector2.left;

        RaycastHit2D[] hits = Physics2D.RaycastAll(player.transform.position, direction, _detectionRange, _objectLayer);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider == null) continue;

            bool hasPushableObject = hit.collider.TryGetComponent(out PushableObject pushableObject);
            if (!hasPushableObject) continue;

            bool isSet = SetNewCurrent(pushableObject, direction);
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
        
        float horizontalInput = player.InputHandler.HorizontalInput;
        float horizontalVelocity = horizontalInput * _pushPullSpeed;

        player.Rb.velocity = new Vector2(horizontalVelocity, player.Rb.velocity.y);
        _currentPushableObject.Move(horizontalVelocity);
    }
}
