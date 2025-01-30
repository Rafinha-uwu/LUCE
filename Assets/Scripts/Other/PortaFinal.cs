using FMOD.Studio;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortaFinal : MonoBehaviour
{
    protected static readonly string PLAYER_TAG = "Player";
    private static readonly string START_SCENE = "StartMenu";

    public GameObject EndCut;
    private EventInstance? _endSound;


    private void Start()
    {
        _endSound = FMODManager.Instance.CreateEventInstance(FMODManager.Instance.EventDatabase.EndGameCutscene);
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
        EndCut.GetComponent<Animator>().Play("Show");
        yield return new WaitForSecondsRealtime(1.5f);

        SaveManager.Instance.NewSave(); // Clear save
        PauseManager.Instance.PauseGame();

        _endSound?.setPaused(false);
        _endSound?.start();

        yield return new WaitForSecondsRealtime(50f);
        _endSound?.stop(STOP_MODE.ALLOWFADEOUT);
        PauseManager.Instance.ResumeGame();
        SceneManager.LoadScene(START_SCENE);
    }

}
