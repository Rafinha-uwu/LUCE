using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fore : MonoBehaviour
{

    public GameObject Foreanim;
    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (once == true)
            {
                Foreanim.GetComponent<Animator>().Play("Idle");
                Invoke("Kill", 4);
                once = false;
            }
        }
    }

    private void Kill()
    {
        Foreanim.SetActive(false);
    }

}
