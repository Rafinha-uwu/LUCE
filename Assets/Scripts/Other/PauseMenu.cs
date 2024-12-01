using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private static readonly string PAUSE_ACTION = "Pause";
    private static readonly string RESUME_ACTION = "Resume";
    private static readonly string START_SCENE = "StartMenu";


    private void Start()
    {
        PauseManager.Instance.PlayerInput.actions[PAUSE_ACTION].performed += OnPause;
        PauseManager.Instance.PlayerInput.actions[RESUME_ACTION].performed += OnPause;

        _canvas.enabled = false;
    }


    private void OnPause(InputAction.CallbackContext context)
    {
        bool isShowingMenu = _canvas.enabled;
        if (isShowingMenu) Resume();
        else if (!PauseManager.Instance.IsPaused) Pause();
    }

    public void Resume()
    {
        _canvas.enabled = false;
        PauseManager.Instance.ResumeGame();
    }

    public void Pause()
    {
        _canvas.enabled = true;
        PauseManager.Instance.PauseGame();
    }


    public void QuitToStart()
    {
        PauseManager.Instance.ResumeGame();
        SceneManager.LoadScene(START_SCENE);
    }

    public void QuitToDesktop()
    {
        if (Application.isEditor) UnityEditor.EditorApplication.isPlaying = false;
        else Application.Quit();
    }
}
