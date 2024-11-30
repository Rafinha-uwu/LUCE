using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }
    public bool IsPaused { get; private set; }

    [HideInInspector]
    public PlayerInput PlayerInput { get; private set; }
    private static readonly string PLAYER_TAG = "Player";
    private static readonly string PLAYER_ACTION_MAP = "Player";
    private static readonly string UI_ACTION_MAP = "UI";


    private void Awake()
    {
        if (Instance != null)
        {
            enabled = false;
            return;
        }
        Instance = this;

        PlayerInput = GameObject.Find(PLAYER_TAG).GetComponent<PlayerInput>();
        ResumeGame();
    }


    public void PauseGame()
    {
        if (IsPaused) return;
        IsPaused = true;

        // Set the time scale to 0 and switch to the UI action map
        Time.timeScale = 0;
        PlayerInput.SwitchCurrentActionMap(UI_ACTION_MAP);
    }

    public void ResumeGame()
    {
        if (!IsPaused) return;
        IsPaused = false;

        // Set the time scale to 1 and switch to the player action map
        Time.timeScale = 1;
        PlayerInput.SwitchCurrentActionMap(PLAYER_ACTION_MAP);
    }

    public void TogglePause()
    {
        if (IsPaused) ResumeGame();
        else PauseGame();
    }
}
