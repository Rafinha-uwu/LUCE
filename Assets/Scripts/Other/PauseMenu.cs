using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private SettingsMenu _settingsMenu;
    [SerializeField] private HintsMenu _hintsMenu;
    [SerializeField] private UnityEngine.UI.Button _firstButton;

    private InputHandler _inputHandler;
    private static readonly string START_SCENE = "StartMenu";


    private void Start()
    {
        _inputHandler = PauseManager.Instance.InputHandler;
        if (_inputHandler == null) return;
        if (_canvas == null) throw new System.Exception("Canvas is not set in the inspector");
        if (_firstButton == null) throw new System.Exception("First button is not set in the inspector");

        _inputHandler.OnPauseAction += OnPause;
        _inputHandler.OnResumeAction += OnPause;

        _canvas.enabled = false;
        if (_settingsMenu != null) _settingsMenu.OnClose += OnSettingsClose;
        if (_hintsMenu != null) _hintsMenu.OnClose += OnHintsClose;
    }

    private void OnDestroy()
    {
        _inputHandler.OnPauseAction -= OnPause;
        _inputHandler.OnResumeAction -= OnPause;
        if (_settingsMenu != null) _settingsMenu.OnClose -= OnSettingsClose;
        if (_hintsMenu != null) _hintsMenu.OnClose -= OnHintsClose;
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
        EventSystem.current.SetSelectedGameObject(null);
        PauseManager.Instance.ResumeGame();
    }

    public void Pause()
    {
        _canvas.enabled = true;
        _firstButton.Select();
        PauseManager.Instance.PauseGame();
    }

    public void Hints()
    {
        if (_hintsMenu == null) return;
        _canvas.enabled = false;
        _hintsMenu.Open();
    }

    private void OnHintsClose()
    {
        if (PauseManager.Instance.IsPaused)
        {
            _canvas.enabled = true;
            _firstButton.Select();
        }
    }


    public void Settings()
    {
        if (_settingsMenu == null) return;
        _canvas.enabled = false;
        _settingsMenu.Open();
    }

    private void OnSettingsClose()
    {
        if (PauseManager.Instance.IsPaused)
        {
            _canvas.enabled = true;
            _firstButton.Select();
        }
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
