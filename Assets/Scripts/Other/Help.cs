using UnityEngine;
using UnityEngine.InputSystem;

public class Help : MonoBehaviour
{
    private Animator animator;
    private bool playerInRange = false;
    public bool isUsingController = false;
    // private bool hasPlayedAnimation = false;

    private void Start()
    {
        animator = GetComponent<Animator>();

        // Subscribe to the input system's device change event
        InputSystem.onActionChange += (obj, change) =>
        {
            if (change == InputActionChange.ActionPerformed)
            {
                InputAction action = (InputAction)obj;
                UpdateInputMethod(action.activeControl.device);
            }
        };
    }

    private void UpdateInputMethod(InputDevice device)
    {
        bool wasUsingController = isUsingController;
        isUsingController = device is Gamepad;

        // Only update animation if the input method changed and player is in range
        if (wasUsingController != isUsingController && playerInRange)
        {
            PlayAppropriateAnimation();
        }
    }

    private void PlayAppropriateAnimation()
    {
        if (isUsingController)
        {
            animator.Play("ControllerHelp");
        }
        else
        {
            animator.Play("KeyboardHelp");
        }
        // hasPlayedAnimation = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            // hasPlayedAnimation = false;
            PlayAppropriateAnimation();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            // hasPlayedAnimation = false;
            // Play different idle animations based on last used input method
            if (isUsingController)
            {
                animator.Play("Idle");
            }
            else
            {
                animator.Play("Idle 1");
            }
        }
    }
}