using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Code : MonoBehaviour
{
    Elevator elevator;
    public GameObject el;


    public bool On = false;

    // Start is called before the first frame update
    void Start()
    {
        elevator = el.GetComponent<Elevator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (On)
        {
            elevator.On = true;
        }
        else
        {

        }
    }
}
