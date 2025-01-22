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

    [SerializeField] private SettingsMenu _settingsMenu;


    private void Start()
    {
        _startGameCutsceneInstance = FMODManager.Instance.CreateEventInstance(FMODManager.Instance.EventDatabase.StartGameCutscene);
    }

    public void NewGame()
    {
        _blackAnimator.SetBool("Dark", true);
        _menuAnimator.SetBool("Start", true);

        _backgroundPlayer.StopBGM();
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
        _settingsMenu.Open();
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


