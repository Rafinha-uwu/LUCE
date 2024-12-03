using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private InputAction _pauseAction;
    private InputAction _resumeAction;

    private static readonly string PAUSE_ACTION = "Pause";
    private static readonly string RESUME_ACTION = "Resume";
    private static readonly string START_SCENE = "StartMenu";


    private void Start()
    {
        _pauseAction = PauseManager.Instance.PlayerInput.actions[PAUSE_ACTION];
        _resumeAction = PauseManager.Instance.PlayerInput.actions[RESUME_ACTION];
        
        _pauseAction.performed += OnPause;
        _resumeAction.performed += OnPause;

        _canvas.enabled = false;
    }

    private void OnDestroy()
    {
        _pauseAction.performed -= OnPause;
        _resumeAction.performed -= OnPause;
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
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
