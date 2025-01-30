using FMOD.Studio;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortaFinal : MonoBehaviour
{
    protected static readonly string PLAYER_TAG = "Player";
    private static readonly string START_SCENE = "StartMenu";

    public GameObject EndCut;
    private Animator _endCutAnimator;
    private EventInstance? _endSound;


    private void Start()
    {
        _endSound = FMODManager.Instance.CreateEventInstance(FMODManager.Instance.EventDatabase.EndGameCutscene);
        _endCutAnimator = EndCut.GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(PLAYER_TAG))
        {
            StartCoroutine(End());
        }
    }

    private IEnumerator End()
    {
        _endCutAnimator.SetBool("Thanks", true);
        _endCutAnimator.Play("Show");
        yield return new WaitForSecondsRealtime(1.5f);

        SaveManager.Instance.NewSave(); // Clear save
        PauseManager.Instance.PauseGame();

        _endSound?.setPaused(false);
        _endSound?.start();

        yield return new WaitForSecondsRealtime(54.5f);
        _endSound?.stop(STOP_MODE.ALLOWFADEOUT);
        PauseManager.Instance.ResumeGame();
        SceneManager.LoadScene(START_SCENE);
    }

}
