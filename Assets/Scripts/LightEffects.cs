using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightEffects : MonoBehaviour
{
    [Header("Flicker Settings")]
    public bool flicker = true;
    [SerializeField] private float LightTimer = 10;
    [SerializeField] private float MaxItensity = 3f;
    [SerializeField] private float DarknessTimer = 1f;


    [Header("Flicker Randomizer")]
    public bool RandomTime = true;
    [SerializeField] private float Min = 7;
    [SerializeField] private float Max = 15;

    [Header("Wigle Settings")]
    public bool wigle = true;
    private bool wigle2 = true;
    private bool dir = true;
    [SerializeField] private float speed = 1;




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

            if (RandomTime)
            {
                LightTimer = Random.Range(Min, Max);

            }

        }

        if (wigle2 == true)
        {
            StartCoroutine(Wig());
        }

        if (wigle == true)
        {
            if (dir == true)
            {

                this.GetComponent<Transform>().Rotate(0, 0, speed);

            }
            else
            {
                this.GetComponent<Transform>().Rotate(0, 0, -speed);

            }
        }

    }


    public IEnumerator Flick1()
    {
        this.GetComponentInChildren<Light2D>().intensity = MaxItensity;

        flicker = false;

        yield return new WaitForSeconds(LightTimer);
        this.GetComponentInChildren<Light2D>().intensity = 1;

        yield return new WaitForSeconds(0.2f);
        this.GetComponentInChildren<Light2D>().intensity = MaxItensity;

        yield return new WaitForSeconds(0.1f);
        this.GetComponentInChildren<Light2D>().intensity = 0;

        yield return new WaitForSeconds(DarknessTimer);
        this.GetComponentInChildren<Light2D>().intensity = 1;

        yield return new WaitForSeconds(0.5f);
        this.GetComponentInChildren<Light2D>().intensity = 3;
        flicker = true;

    }

    public IEnumerator Wig()
    {
        wigle2 = false;
        dir = true;
        yield return new WaitForSeconds(2f);

        dir = false;

        yield return new WaitForSeconds(4f);

        dir = true;

        yield return new WaitForSeconds(2f);


        wigle2 = true;

    }
}
