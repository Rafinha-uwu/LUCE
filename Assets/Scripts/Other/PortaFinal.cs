using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortaFinal : MonoBehaviour
{

    protected static readonly string PLAYER_TAG = "Player";
    private static readonly string START_SCENE = "StartMenu";

    public GameObject EndCut;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

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

        PauseManager.Instance.PauseGame();

        yield return new WaitForSecondsRealtime(50f);
        PauseManager.Instance.ResumeGame();
        SceneManager.LoadScene(START_SCENE);

    }

}
