using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHelp : MonoBehaviour
{

    public GameObject HelpDoor;
    public GameObject HelpKey;

    public GameObject Key;

    public bool keyreal;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!Key.activeSelf && HelpDoor != null)
        {
            keyreal = false;

            if (HelpDoor.GetComponent<Help>().isUsingController == true)
            {
                HelpDoor.GetComponent<Animator>().Play("Idle");
            }
            else
            {
                HelpDoor.GetComponent<Animator>().Play("Idle 1");
            }

            Invoke("Kill", 1);

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (keyreal)
        {
            if (collision.name == "Key")
            {
                HelpKey.SetActive(true);
                HelpDoor.SetActive(false);
            }
            else
            {
                HelpKey.SetActive(false);
                HelpDoor.SetActive(true);
            }
        }


    }

    public void Kill()
    {
        Destroy(HelpKey);
        Destroy(HelpDoor);
    }
}
