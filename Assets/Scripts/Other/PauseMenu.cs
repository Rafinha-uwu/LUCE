using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private SettingsMenu _settingsMenu;

    private InputHandler _inputHandler;
    private static readonly string START_SCENE = "StartMenu";


    private void Start()
    {
        _inputHandler = PauseManager.Instance.InputHandler;
        if (_inputHandler == null) return;

        _inputHandler.OnPauseAction += OnPause;
        _inputHandler.OnResumeAction += OnPause;

        _canvas.enabled = false;
        if (_settingsMenu != null) _settingsMenu.OnClose += OnSettingsClose;
    }

    private void OnDestroy()
    {
        _inputHandler.OnPauseAction -= OnPause;
        _inputHandler.OnResumeAction -= OnPause;
        if (_settingsMenu != null) _settingsMenu.OnClose -= OnSettingsClose;
    }


    private void OnPause()
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


    public void Settings()
    {
        if (_settingsMenu == null) return;
        _canvas.enabled = false;
        _settingsMenu.Open();
    }

    private void OnSettingsClose()
    {
        _canvas.enabled = true;
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
