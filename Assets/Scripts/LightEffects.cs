using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightEffects : MonoBehaviour
{


    public bool flicker = true;
    [SerializeField] float fn = 10;

    public bool wigle = true;
    public bool dir = true;
    [SerializeField] float speed = 1;




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (flicker == true)
        {
            StartCoroutine(Flick1());
            fn = Random.Range(7, 15);
        }

        if (wigle == true)
        {
            StartCoroutine(Wig());
        }


        if (dir == true)
        {

            this.GetComponent<Transform>().Rotate(0, 0, speed);

        }
        else
        {
            this.GetComponent<Transform>().Rotate(0, 0, -speed);

        }


    }


    public IEnumerator Flick1()
    {
        this.GetComponentInChildren<Light2D>().intensity = 3;

        flicker = false;

        yield return new WaitForSeconds(fn);
        this.GetComponentInChildren<Light2D>().intensity = 1;

        yield return new WaitForSeconds(0.2f);
        this.GetComponentInChildren<Light2D>().intensity = 3;

        yield return new WaitForSeconds(0.1f);
        this.GetComponentInChildren<Light2D>().intensity = 0;

        yield return new WaitForSeconds(1f);
        this.GetComponentInChildren<Light2D>().intensity = 1;

        yield return new WaitForSeconds(0.5f);
        this.GetComponentInChildren<Light2D>().intensity = 3;
        flicker = true;

    }

    public IEnumerator Wig()
    {
        wigle = false;
        dir = true;
        yield return new WaitForSeconds(2f);

        dir = false;

        yield return new WaitForSeconds(4f);

        dir = true;

        yield return new WaitForSeconds(2f);


        wigle = true;

    }
}
