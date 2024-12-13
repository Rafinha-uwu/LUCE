using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Polaroid : MonoBehaviour
{

    public GameObject Player;
    public GameObject Colect;
    public GameObject Black;

    private CountColect count;
    public bool On;
    public bool Narrative1 = false;

    // Start is called before the first frame update
    void Start()
    {
        count = Colect.GetComponent<CountColect>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (On)
        {
            if (transform.IsChildOf(Player.transform))
            {
                PauseManager.Instance.PauseGame();
                if (Narrative1)
                {
                    Colect.GetComponent<Animator>().SetBool("Nar1", true);
                    Black.GetComponent<Animator>().SetBool("Dark", true);

                    StartCoroutine(Die1());
                }
                else
                {
                    Colect.GetComponent<Animator>().SetBool("Found", true);
                    Black.GetComponent<Animator>().SetBool("Dark", true);

                    StartCoroutine(Die());
                }
            }
        }
    }


    public IEnumerator Die()
    {
        On = false;
        yield return new WaitForSecondsRealtime(1f);
        Colect.GetComponent<Animator>().SetBool("Found", false);
        yield return new WaitForSecondsRealtime(2.5f);
        count.nColect++;
        yield return new WaitForSecondsRealtime(1f);
        Black.GetComponent<Animator>().SetBool("Dark", false);

        Player.GetComponent<PlayerHoldItem>().ForceDrop();


        yield return new WaitForSecondsRealtime(0.5f);
        PauseManager.Instance.ResumeGame();
        Destroy(this.gameObject);
    }
    public IEnumerator Die1()
    {
        On = false;
        yield return new WaitForSecondsRealtime(1f);
        Colect.GetComponent<Animator>().SetBool("Nar1", false);
        yield return new WaitForSecondsRealtime(2.5f);
        count.nColect++;
        yield return new WaitForSecondsRealtime(20f);
        Black.GetComponent<Animator>().SetBool("Dark", false);

        Player.GetComponent<PlayerHoldItem>().ForceDrop();

        yield return new WaitForSecondsRealtime(0.5f);
        Player.GetComponent<Scared>().Speed_scared = 220;
        PauseManager.Instance.ResumeGame();
        Destroy(this.gameObject);
    }

}
