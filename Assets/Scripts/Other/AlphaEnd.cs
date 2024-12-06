using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlphaEnd : MonoBehaviour
{
    private static readonly string SCENE_TO_GO = "StartMenu";
    public bool Once = true;

    public GameObject White;
    public GameObject Pm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && Once == true)
        {

            White.GetComponent<Animator>().SetBool("White", true);
            Once = false;

            StartCoroutine(Reset());
        }
    }

    public IEnumerator Reset()
    {
        yield return new WaitForSeconds(1);
        PauseManager.Instance.PauseGame();
        yield return new WaitForSecondsRealtime(20);
        SceneManager.LoadScene(SCENE_TO_GO);
    }
}
