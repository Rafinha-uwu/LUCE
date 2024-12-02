using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFall : MonoBehaviour
{
    private Scared scared;
    public GameObject boo;

    public GameObject Black;

    void Start()
    {

        scared = boo.GetComponent<Scared>();


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Black.GetComponent<Animator>().SetBool("Dark", true);
            Invoke("FallDeath", 0.5f);


        }
    }

    private void FallDeath()
    {
        Black.GetComponent<Animator>().SetBool("Dark", false);
        scared.Death();
    }
}