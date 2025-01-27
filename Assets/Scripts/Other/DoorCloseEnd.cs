using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCloseEnd : MonoBehaviour
{
    // Start is called before the first frame update


    private static readonly string PLAYER_TAG = "Player";


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(PLAYER_TAG)) return;

        this.GetComponentInChildren<Animator>().Play("CloseDoor");

    }
}
