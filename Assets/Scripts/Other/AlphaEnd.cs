using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlphaEnd : MonoBehaviour
{

    public bool Once = true;

    public GameObject White;
    public GameObject Pm;

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
        yield return new WaitForSeconds(20);
        SceneManager.LoadScene("Main");
    }
}

