using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class TP : MonoBehaviour, ISavable
{
    public GameObject Pol;

    public GameObject Player;

    public GameObject Light1;
    public GameObject Light2;

    public GameObject White;

    public GameObject TPANIM;

    private bool Once = true;

    // Update is called once per frame
    private void Update()
    {
        if (!Pol.activeSelf && Once == true)
        {
            StartCoroutine(TPME());
            Once = false;
        }
    }

    private IEnumerator TPME()
    {
        yield return new WaitForSeconds(1);

        Light1.GetComponent<Light2D>().intensity = 2;
        Light2.GetComponent<Light2D>().intensity = 1;

        yield return new WaitForSeconds(1);

        Light1.GetComponent<Light2D>().intensity = 0.5f;
        Light2.GetComponent<Light2D>().intensity = 0.5f;

        yield return new WaitForSeconds(0.3f);

        Light1.GetComponent<Light2D>().intensity = 2;
        Light2.GetComponent<Light2D>().intensity = 1;


        yield return new WaitForSeconds(0.6f);

        Light1.GetComponent<Light2D>().intensity = 0f;
        Light2.GetComponent<Light2D>().intensity = 0f;


        yield return new WaitForSeconds(4);

        TPANIM.GetComponent<Animator>().Play("Text");

        yield return new WaitForSeconds(0.1f);
        PauseManager.Instance.PauseGame();


        yield return new WaitForSecondsRealtime(2.8f);
        PauseManager.Instance.ResumeGame();

        yield return new WaitForSeconds(2.1f);
        PauseManager.Instance.PauseGame();

        yield return new WaitForSecondsRealtime(4.9f);
        PauseManager.Instance.ResumeGame();


        yield return new WaitForSeconds(1f);

        White.GetComponent<Animator>().SetBool("White", true);

        yield return new WaitForSeconds(4f);

        Player.gameObject.transform.position = new Vector3(924.7f, -8.96f, 0);
        Player.GetComponent<Scared>().enabled = false;

        yield return new WaitForSeconds(4);

        White.GetComponent<Animator>().SetBool("White", false);
    }


    public string GetSaveName() => name;
    public object GetSaveData() => Once;
    public void LoadData(object data)
    {
        bool savedOnce = (bool)data;
        if (Once) Once = savedOnce;
    }
}