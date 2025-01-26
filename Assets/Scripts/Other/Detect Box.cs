using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectBox : MonoBehaviour
{

    public GameObject Help;




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

        if(collision.name == "Box")
        {
            if (Help.GetComponent<Help>().isUsingController == true)
            {
                Help.GetComponent<Animator>().Play("Idle");
            }
            else
            {
                Help.GetComponent<Animator>().Play("Idle 1");
            }
            Invoke("Kill", 1);
        }

    }
    public void Kill()
    {
        Destroy(Help);
    }
}
