using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;

    private InputHandler _inputHandler;
    private static readonly string START_SCENE = "StartMenu";


    private void Start()
    {
        _inputHandler = PauseManager.Instance.InputHandler;
        if (_inputHandler == null) return;

        _inputHandler.OnPauseAction += OnPause;
        _inputHandler.OnResumeAction += OnPause;

        _canvas.enabled = false;
    }

    private void OnDestroy()
    {
        _inputHandler.OnPauseAction -= OnPause;
        _inputHandler.OnResumeAction -= OnPause;
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
