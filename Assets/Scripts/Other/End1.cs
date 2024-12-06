using System.Collections;
using UnityEngine;

public class End1 : SwitchObject
{
    private static readonly string PLAYER_TAG = "Player";
    public GameObject cLights;

    public GameObject Lights1;
    public GameObject Lights2;
    public GameObject Lights3;
    public GameObject Lights4;
    public GameObject Lights5;
    public GameObject Lights6;
    public GameObject Lights7;
    public GameObject Lights8;
    public GameObject Lights9;
    public GameObject Lights10;

    protected override void Start()
    {
        IsOn = false;
        base.Start();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool polaroidTaken = transform.childCount < 1;
        bool playerInside = collision.CompareTag(PLAYER_TAG);

        if (playerInside && polaroidTaken && !IsOn) StartCoroutine(Run());
    }

    public IEnumerator Run()
    {
        TurnOn();
        cLights.SetActive(true);

        yield return new WaitForSeconds(1);
        cLights.SetActive(false);
        Lights8.SetActive(false);
        Lights9.SetActive(false);
        Lights10.SetActive(false);

        yield return new WaitForSeconds(0.2f);
        cLights.SetActive(true);

        yield return new WaitForSeconds(0.1f);
        cLights.SetActive(false);

        yield return new WaitForSeconds(2f);

        Lights1.SetActive(true);
        Lights2.SetActive(true);
        Lights3.SetActive(true);
        Lights4.SetActive(true);
        Lights5.SetActive(true);
        Lights6.SetActive(true);
        Lights7.SetActive(true);


        yield return new WaitForSeconds(0.2f);


        Lights1.SetActive(false);
        Lights2.SetActive(false);


        yield return new WaitForSeconds(0.1f);

        Lights1.SetActive(true);
        Lights2.SetActive(true);


    }
}
