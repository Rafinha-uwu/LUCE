using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shut : MonoBehaviour
{

    public GameObject Block;
    public GameObject Plate;
    public GameObject Door;
    public GameObject Black;

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
        if(collision.tag == "Player")
        {

            Door.GetComponent<Animator>().SetBool("Open", false);
            Door.GetComponent<Animator>().SetBool("Reset", true);
            Block.SetActive(true);
            Plate.SetActive(false);
            Black.GetComponent<Animator>().Play("100");


        }


    }

}
