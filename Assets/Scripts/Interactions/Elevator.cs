using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    public bool On = true;
    public bool Top = true;

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

        if (collision.tag == "Player")
        {
            this.GetComponent<Animator>().SetBool("Shake", true);


            if (On == true)
            {
                if(Top == true)
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

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            this.GetComponent<Animator>().SetBool("Shake", false);

        }

    }

}