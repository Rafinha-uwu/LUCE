using FMOD.Studio;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private static readonly string GAME_SCENE = "Main";

    [SerializeField] private Animator _menuAnimator;
    [SerializeField] private Animator _blackAnimator;

    [SerializeField] private StartMenuBackgroundPlayer _backgroundPlayer;
    private EventInstance? _startGameCutsceneInstance;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private SettingsMenu _settingsMenu;


    private void Awake()
    {
        if (_canvas == null) throw new System.NullReferenceException("Canvas is not set in the inspector");

        _canvas.enabled = true;
        if (_settingsMenu != null) _settingsMenu.OnClose += OnSettingsClose;
    }

    private void Start()
    {
        _startGameCutsceneInstance = FMODManager.Instance.CreateEventInstance(FMODManager.Instance.EventDatabase.StartGameCutscene);
    }

    private void OnDestroy()
    {
        if (_settingsMenu != null) _settingsMenu.OnClose -= OnSettingsClose;
    }


    public void NewGame()
    {
        if (_blackAnimator != null) _blackAnimator.SetBool("Dark", true);
        if (_menuAnimator != null) _menuAnimator.SetBool("Start", true);

        if (_backgroundPlayer != null) _backgroundPlayer.StopBGM();
        _startGameCutsceneInstance?.start();

        Invoke(nameof(Load), 16);
    }

    public void Load()
    {
        _startGameCutsceneInstance?.stop(STOP_MODE.ALLOWFADEOUT);
        SceneManager.LoadScene(GAME_SCENE);
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


    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}


