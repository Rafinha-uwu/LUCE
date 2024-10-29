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


    public override void EnterState(PlayerController player)
    {
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

        CurrentObjectCheck();
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

        GameObject.Find("Player").GetComponent<Flip>().enabled = true;
    }

    private void CurrentObjectCheck()
    {
        if (_currentPushableObject == null) return;
        if (!_currentPushableObject.IsGrounded) CurrentStopPushing();
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
            if (!hasPushableObject || !pushableObject.IsGrounded) continue;

            _currentPushableObject = pushableObject;
            _currentPushableObject.StartPushPull(_objectMass, _objectMaterial);
           
            GameObject.Find("Player").GetComponent<Flip>().enabled = false;
            break;
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
