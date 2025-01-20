using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private static readonly string GAME_SCENE = "Main";

    [SerializeField] private Animator _menuAnimator;
    [SerializeField] private Animator _blackAnimator;


    public void NewGame()
    {
        _blackAnimator.SetBool("Dark", true);
        _menuAnimator.SetBool("Start", true);

        Invoke(nameof(Load), 16);
    }

    public void Load()
    {
        SceneManager.LoadScene(GAME_SCENE);
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


