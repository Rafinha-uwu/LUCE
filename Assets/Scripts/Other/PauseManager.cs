using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }
    public bool IsPaused { get; private set; }

    [HideInInspector]
    public InputHandler InputHandler { get; private set; }
    private static readonly string PLAYER_TAG = "Player";


    private void Awake()
    {
        if (Instance != null) Instance.enabled = false;
        Instance = this;

        InputHandler = GameObject.FindGameObjectWithTag(PLAYER_TAG).GetComponent<InputHandler>();
        ResumeGame();
    }

    private void Start() => ResumeGame();


    public void PauseGame(bool pauseSounds = true)
    {
        if (IsPaused) return;
        IsPaused = true;

        // Set the time scale to 0 and pause the player input
        Time.timeScale = 0;
        if (InputHandler != null) InputHandler.PauseInput(this);
        if (pauseSounds) FMODManager.Instance.PauseSounds();
    }

    public void ResumeGame()
    {
        if (!IsPaused) return;
        IsPaused = false;

        // Set the time scale to 1 and resume the player input
        Time.timeScale = 1;
        if (InputHandler != null) InputHandler.ResumeInput(this);
        FMODManager.Instance.ResumeSounds();
    }

    public void TogglePause()
    {
        if (IsPaused) ResumeGame();
        else PauseGame();
    }
}
