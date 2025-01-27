using FMOD.Studio;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlphaEnd : MonoBehaviour
{
    private static readonly string SCENE_TO_GO = "StartMenu";
    private static readonly string PLAYER_TAG = "Player";

    private bool _once = true;

    public GameObject White;
    private Animator _whiteAnimator;

    private EventInstance? _endSound;


    private void Start()
    {
        _whiteAnimator = White.GetComponent<Animator>();
        _endSound = FMODManager.Instance.CreateEventInstance(FMODManager.Instance.EventDatabase.EndGameCutscene);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(PLAYER_TAG) || !_once) return;
        _once = false;

        _whiteAnimator.SetBool("White", true);

        StartCoroutine(Reset());
    }

    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(1);
        PauseManager.Instance.PauseGame();
        SaveManager.Instance.NewSave(); // Clear save

        _endSound?.setPaused(false);
        _endSound?.start();

        yield return new WaitForSecondsRealtime(20);
        _endSound?.stop(STOP_MODE.ALLOWFADEOUT);
        PauseManager.Instance.ResumeGame();
        SceneManager.LoadScene(SCENE_TO_GO);
    }
}
