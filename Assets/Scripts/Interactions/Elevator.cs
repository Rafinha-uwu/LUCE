using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Elevator : MonoBehaviour
{

    public bool On;
    public bool Top = true;
    public bool Mid = true;
    public bool Mid2 = true;

    private Light2D childLight;
    public Color Red;
    public Color Green;


    // Start is called before the first frame update
    void Start()
    {
        childLight = GetComponentInChildren<Light2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (On == true)
        {
            childLight.color = Green;
        }

        else
        {
            childLight.color = Red;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            collision.transform.SetParent(this.transform);
            this.GetComponent<Animator>().SetBool("Shake", true);


            if (On == true)
            {
                if (Mid == true)
                {
                    this.GetComponent<Animator>().SetBool("MID", true);
                    StartCoroutine(Broken());
                }
                else if (Mid2 == true)
                {
                    this.GetComponent<Animator>().SetBool("MIDD", true);
                    Mid2 = false;
                }
                else
                {
                    if (Top == true)
                    {
                        this.GetComponent<Animator>().SetBool("DOWN", true);
                        this.GetComponent<Animator>().SetBool("UP", false);
                        Top = false;
                    }
                    else
                    {
                        this.GetComponent<Animator>().SetBool("DOWN", false);
                        this.GetComponent<Animator>().SetBool("UP", true);
                        Top = true;
                    }

                }
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            this.GetComponent<Animator>().SetBool("Shake", false);
            collision.transform.SetParent(null);
        }

    }

    public IEnumerator Broken()
    {
        yield return new WaitForSeconds(10f);
        On = false;
        Mid = false;


    }

}