using FMOD.Studio;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    private static readonly string GAME_SCENE = "Main";

    [SerializeField] private Animator _menuAnimator;
    [SerializeField] private Animator _blackAnimator;

    [SerializeField] private StartMenuBackgroundPlayer _backgroundPlayer;
    private EventInstance? _startGameCutsceneInstance;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private SettingsMenu _settingsMenu;

    [SerializeField] private GameObject _continueButton;
    [SerializeField] private UnityEngine.UI.Button _firstButton;


    private void Awake()
    {
        if (_canvas == null) throw new System.Exception("Canvas is not set in the inspector");
        if (_firstButton == null) throw new System.Exception("FirstButton is not set in the inspector");

        _canvas.enabled = true;
        if (_settingsMenu != null) _settingsMenu.OnClose += OnSettingsClose;
    }

    private void Start()
    {
        _startGameCutsceneInstance = FMODManager.Instance.CreateEventInstance(FMODManager.Instance.EventDatabase.StartGameCutscene);
        
        FMODManager.Instance.PauseSounds();
        StartCoroutine(StopExternalSounds());

        if (_continueButton == null) return;
        bool saveExists = SaveManager.Instance.SaveExists();
        _continueButton.SetActive(saveExists);

        if (!saveExists) return;
        UnityEngine.UI.Button continueButtonUI = _continueButton.GetComponent<UnityEngine.UI.Button>();

        _firstButton.navigation = new Navigation {
            mode = Navigation.Mode.Explicit,
            selectOnUp = continueButtonUI,
            selectOnDown = _firstButton.navigation.selectOnDown
        };

        _firstButton = continueButtonUI;
    }

    private void OnDestroy()
    {
        if (_settingsMenu != null) _settingsMenu.OnClose -= OnSettingsClose;
    }


    public void NewGame()
    {
        SaveManager.Instance.NewSave(); // Clear save

        if (_blackAnimator != null) _blackAnimator.SetBool("Dark", true);
        if (_menuAnimator != null) _menuAnimator.SetBool("Start", true);

        if (_backgroundPlayer != null) _backgroundPlayer.StopBGM();

        _startGameCutsceneInstance?.start();
        _startGameCutsceneInstance?.setPaused(false);

        Invoke(nameof(Load), 40.5f);
    }

    public void Load()
    {
        if (_backgroundPlayer != null) _backgroundPlayer.StopBGM();
        _startGameCutsceneInstance?.stop(STOP_MODE.ALLOWFADEOUT);

        FMODManager.Instance.ResumeSounds();

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
        _firstButton.Select();
    }


    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }


    public void OnAnimationEnd() => _firstButton.Select();


    private IEnumerator StopExternalSounds()
    {
        yield return new WaitForEndOfFrame();
        if (_backgroundPlayer != null) _backgroundPlayer.PlayBGM();
    }
}


