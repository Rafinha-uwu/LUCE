using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathFall : MonoBehaviour
{
    private Scared scared;
    public GameObject boo;

    void Start()
    {

        scared = boo.GetComponent<Scared>();


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            scared.ScaredCountDown = 0;

        }
    }
}